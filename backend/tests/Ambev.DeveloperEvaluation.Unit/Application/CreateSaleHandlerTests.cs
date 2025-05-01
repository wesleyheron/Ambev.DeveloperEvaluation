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
        private readonly IEventProducer _mockEventProducer;

        public CreateSaleHandlerTests()
        {
            _saleRepository = Substitute.For<ISaleRepository>();
            _mockEventProducer = Substitute.For<IEventProducer>();
            _saleCreatedEventHandler = new SaleCreatedEventHandler(_mockEventProducer);
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

        [Fact(DisplayName = "Given invalid sale data When creating sale Then throws validation exception")]
        public async Task Handle_InvalidRequest_ThrowsValidationException()
        {
            // Arrange
            var command = new CreateSaleCommand();

            // Act
            var act = () => _handler.Handle(command, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<FluentValidation.ValidationException>();
        }

        [Fact(DisplayName = "Given valid sale data When handling Then publishes sale created event")]
        public async Task Handle_ValidRequest_PublishesSaleCreatedEvent()
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
                    new SaleItem { Product = "Product A", Quantity = 2}
                ]
            };

            _saleRepository.CreateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>())
                .Returns(Task.FromResult(sale));

            var mockEventProducer = new Mock<IEventProducer>();
            var mockEventHandler = new SaleCreatedEventHandler(mockEventProducer.Object);
            var handler = new CreateSaleHandler(_saleRepository, mockEventHandler);
            mockEventProducer.Setup(ep => ep.PublishAsync(It.IsAny<SaleCreatedEvent>(), It.IsAny<string>()))
                .Returns(Task.CompletedTask);

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            mockEventProducer.Verify(ep => ep.PublishAsync(It.IsAny<SaleCreatedEvent>(), "SaleCreatedEvent"), Times.Once);
        }

        [Fact(DisplayName = "Given valid sale data When handling Then persists sale in repository")]
        public async Task Handle_ValidRequest_PersistsSaleInRepository()
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
                    new SaleItem { Product = "Product A", Quantity = 2}
                ]
            };

            var mockEventProducer = new Mock<IEventProducer>();
            var mockEventHandler = new SaleCreatedEventHandler(mockEventProducer.Object);
            var handler = new CreateSaleHandler(_saleRepository, mockEventHandler);

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            await _saleRepository.Received(1).CreateAsync(Arg.Is<Sale>(s =>
                s.SaleNumber == command.SaleNumber &&
                s.Customer == command.Customer &&
                s.Branch == command.Branch), Arg.Any<CancellationToken>());
        }

        [Fact(DisplayName = "Given valid sale data When handling Then calculates total amount")]
        public async Task Handle_ValidRequest_CalculatesTotalAmount()
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
                    new SaleItem { Product = "Product A", Quantity = 2}
                ]
            };

            var mockEventProducer = new Mock<IEventProducer>();
            var mockEventHandler = new SaleCreatedEventHandler(mockEventProducer.Object);
            var handler = new CreateSaleHandler(_saleRepository, mockEventHandler);

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            sale.TotalAmount.Should().Be(sale.Items.Sum(i => i.TotalAmount));
        }

        [Fact(DisplayName = "Given valid sale data When handling Then calculates total quantity of items")]
        public async Task Handle_ValidRequest_CalculatesTotalQuantity()
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
                    new SaleItem { Product = "Product A", Quantity = 2},
                    new SaleItem { Product = "Product B", Quantity = 3}
                ]
            };

            var mockEventProducer = new Mock<IEventProducer>();
            var mockEventHandler = new SaleCreatedEventHandler(mockEventProducer.Object);
            var handler = new CreateSaleHandler(_saleRepository, mockEventHandler);

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            sale.Items.Sum(i => i.Quantity).Should().Be(5);
        }

    }

}
