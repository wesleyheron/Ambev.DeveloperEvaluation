using Ambev.DeveloperEvaluation.Application.Sales.Commands.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.Events;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Events.Sales;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.MessageBroker.Abstractions;
using Ambev.DeveloperEvaluation.Unit.Application.TestData;
using FluentAssertions;
using Moq;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application
{
    public class CreateSaleHandlerTests
    {
        private readonly ISaleRepository _saleRepository;
        private readonly SaleCreatedEventHandler _saleCreatedEventHandler;
        private readonly CreateSaleHandler _handler;

        public CreateSaleHandlerTests()
        {
            _saleRepository = Substitute.For<ISaleRepository>();
            var mockEventProducer = new Mock<IEventProducer>();
            _saleCreatedEventHandler = new SaleCreatedEventHandler(mockEventProducer.Object);
            _handler = new CreateSaleHandler(_saleRepository, _saleCreatedEventHandler);
        }

        [Fact]
        public async Task Handle_ShouldPublishEvent_WhenSaleCreatedEventIsHandled()
        {
            // Arrange
            var mockEventProducer = new Mock<IEventProducer>();
            var handler = new SaleCreatedEventHandler(mockEventProducer.Object);

            var saleCreatedEvent = new SaleCreatedEvent(new Sale());

            mockEventProducer
                .Setup(ep => ep.PublishAsync(It.IsAny<SaleCreatedEvent>(), It.IsAny<string>()))
                .Returns(Task.CompletedTask);

            // Act
            await handler.Handle(saleCreatedEvent, CancellationToken.None);

            // Assert
            mockEventProducer.Verify(ep => ep.PublishAsync(It.IsAny<SaleCreatedEvent>(), "SaleCreatedEvent"), Times.Once);
        }

        [Fact(DisplayName = "Given valid sale data When creating sale Then returns success response")]
        public async Task Handle_ValidRequest_ReturnsSuccessResponse()
        {
            // Arrange
            var command = CreateSaleHandlerTestData.GenerateValidCommand();

            var saleId = Guid.NewGuid();
            var sale = new Sale
            {
                Id = saleId,
                SaleNumber = command.SaleNumber,
                Customer = command.Customer,
                Branch = command.Branch,
                Items =
                [
                    new SaleItem { Product = "Product A", Quantity = 2 }
                ],
            };

            _saleRepository.CreateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>())
                .Returns(Task.FromResult(sale));

            var mockEventProducer = new Mock<IEventProducer>();
            var mockEventHandler = new SaleCreatedEventHandler(mockEventProducer.Object);
            mockEventProducer.Setup(ep => ep.PublishAsync(It.IsAny<SaleCreatedEvent>(), It.IsAny<string>()))
                .Returns(Task.CompletedTask);

            // Act
            var response = await _handler.Handle(command, CancellationToken.None);

            // Assert
            response.Should().NotBeNull();
            await _saleRepository.Received(1).CreateAsync(Arg.Is<Sale>(s =>
                s.SaleNumber == command.SaleNumber &&
                s.Customer == command.Customer &&
                s.Branch == command.Branch), Arg.Any<CancellationToken>());
        }




        //[Fact(DisplayName = "Given invalid sale data When creating sale Then throws validation exception")]
        //public async Task Handle_InvalidRequest_ThrowsValidationException()
        //{
        //    // Given
        //    var command = new CreateSaleCommand(); // Comando vazio para falhar na validação

        //    // When
        //    var act = () => _handler.Handle(command, CancellationToken.None);

        //    // Then
        //    await act.Should().ThrowAsync<FluentValidation.ValidationException>();
        //}

        //[Fact(DisplayName = "Given valid sale data When handling Then publishes sale created event")]
        //public async Task Handle_ValidRequest_PublishesSaleCreatedEvent()
        //{
        //    // Given
        //    var command = CreateSaleHandlerTestData.GenerateValidCommand();
        //    var sale = CreateSaleHandlerTestData.GenerateValidSale(command);

        //    _saleRepository.CreateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>())
        //        .Returns(Task.CompletedTask);

        //    // When
        //    await _handler.Handle(command, CancellationToken.None);

        //    // Then
        //    await _saleCreatedEventHandler.Received(1).Handle(
        //        Arg.Is<SaleCreatedEvent>(e => e.Sale.Id == sale.Id), Arg.Any<CancellationToken>());
        //}

        //[Fact(DisplayName = "Given valid sale data When handling Then persists sale in repository")]
        //public async Task Handle_ValidRequest_PersistsSaleInRepository()
        //{
        //    // Given
        //    var command = CreateSaleHandlerTestData.GenerateValidCommand();
        //    var sale = CreateSaleHandlerTestData.GenerateValidSale(command);

        //    // When
        //    await _handler.Handle(command, CancellationToken.None);

        //    // Then
        //    await _saleRepository.Received(1).CreateAsync(Arg.Is<Sale>(s =>
        //        s.SaleNumber == command.SaleNumber &&
        //        s.Customer == command.Customer &&
        //        s.Branch == command.Branch), Arg.Any<CancellationToken>());
        //}

        //[Fact(DisplayName = "Given valid sale data When handling Then calculates total amount")]
        //public async Task Handle_ValidRequest_CalculatesTotalAmount()
        //{
        //    // Given
        //    var command = CreateSaleHandlerTestData.GenerateValidCommand();
        //    var sale = CreateSaleHandlerTestData.GenerateValidSale(command);

        //    // When
        //    await _handler.Handle(command, CancellationToken.None);

        //    // Then
        //    sale.TotalAmount.Should().Be(sale.Items.Sum(i => i.TotalAmount));
        //}
    }

}
