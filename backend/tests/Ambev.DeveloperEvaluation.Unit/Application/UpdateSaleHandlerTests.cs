using Ambev.DeveloperEvaluation.Application.Sales.Commands.UpdateSale;
using Ambev.DeveloperEvaluation.Application.Sales.Events;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Events.Sales;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.MessageBroker.Abstractions;
using FluentAssertions;
using Moq;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application
{
    public class UpdateSaleHandlerTests
    {
        private readonly ISaleRepository _saleRepository;
        private readonly SaleModifiedEventHandler _saleModifiedEventHandler;
        private readonly UpdateSaleHandler _handler;
        private readonly IEventProducer _mockEventProducer;

        public UpdateSaleHandlerTests()
        {
            _saleRepository = Substitute.For<ISaleRepository>();
            _mockEventProducer = Substitute.For<IEventProducer>();
            _saleModifiedEventHandler = new SaleModifiedEventHandler(_mockEventProducer);
            _handler = new UpdateSaleHandler(_saleRepository, _saleModifiedEventHandler);
        }

        [Fact(DisplayName = "Given valid sale data When updating sale Then returns updated sale result")]
        public async Task Handle_ValidRequest_ReturnsUpdatedSaleResult()
        {
            // Arrange
            var command = CreateUpdateSaleCommand();

            var sale = new Sale
            {
                Id = command.Id,
                SaleNumber = "123",
                Customer = "Customer A",
                Branch = "Branch 1",
                Items =
                [
                    new SaleItem { Product = "Product A", Quantity = 1, UnitPrice = 100 }
                ]
            };

            _saleRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>())
                .Returns(Task.FromResult(sale));

            _saleRepository.UpdateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>())
                .Returns(Task.FromResult(sale));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(command.Id);
            result.SaleNumber.Should().Be(sale.SaleNumber);
            result.Customer.Should().Be(sale.Customer);
            result.Branch.Should().Be(sale.Branch);
            result.TotalAmount.Should().Be(sale.TotalAmount);
        }

        [Fact(DisplayName = "Given invalid sale data When updating sale Then throws validation exception")]
        public async Task Handle_InvalidRequest_ThrowsValidationException()
        {
            // Arrange
            var command = new UpdateSaleCommand(Guid.NewGuid(), "", "", "", false);

            // Act
            var act = () => _handler.Handle(command, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<FluentValidation.ValidationException>();
        }

        [Fact(DisplayName = "Given non-existent sale When updating sale Then throws KeyNotFoundException")]
        public async Task Handle_SaleNotFound_ThrowsKeyNotFoundException()
        {
            // Arrange
            var command = CreateUpdateSaleCommand();

            _saleRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>())
                .Returns(Task.FromResult<Sale>(null));

            // Act
            var act = () => _handler.Handle(command, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<KeyNotFoundException>()
                .WithMessage($"Sale with ID {command.Id} not found");
        }

        [Fact(DisplayName = "Given valid sale data When updating sale Then publishes sale modified event")]
        public async Task Handle_ValidRequest_PublishesSaleModifiedEvent()
        {
            // Arrange
            var command = CreateUpdateSaleCommand();

            var sale = new Sale
            {
                Id = command.Id,
                SaleNumber = "123",
                Customer = "Customer A",
                Branch = "Branch 1"
            };

            _saleRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>())
                .Returns(Task.FromResult(sale));

            _saleRepository.UpdateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>())
                .Returns(Task.FromResult(sale));

            var mockEventProducer = new Mock<IEventProducer>();
            var mockEventHandler = new SaleModifiedEventHandler(mockEventProducer.Object);
            var handler = new UpdateSaleHandler(_saleRepository, mockEventHandler);
            mockEventProducer.Setup(ep => ep.PublishAsync(It.IsAny<SaleModifiedEvent>(), It.IsAny<string>()))
                .Returns(Task.CompletedTask);

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            mockEventProducer.Verify(ep => ep.PublishAsync(It.IsAny<SaleModifiedEvent>(), "SaleModifiedEvent"), Times.Once);
        }

        [Fact(DisplayName = "Given valid sale data When updating sale Then persists updated sale in repository")]
        public async Task Handle_ValidRequest_PersistsUpdatedSaleInRepository()
        {
            // Arrange
            var command = CreateUpdateSaleCommand();

            var sale = new Sale
            {
                Id = command.Id,
                SaleNumber = "123",
                Customer = "Customer A",
                Branch = "Branch 1"
            };

            _saleRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>())
                .Returns(Task.FromResult(sale));

            _saleRepository.UpdateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>())
                .Returns(Task.FromResult(sale));

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            await _saleRepository.Received(1).UpdateAsync(Arg.Is<Sale>(s =>
                s.SaleNumber == command.SaleNumber &&
                s.Customer == command.Customer &&
                s.Branch == command.Branch), Arg.Any<CancellationToken>());
        }

        [Fact(DisplayName = "Given cancelled sale When updating sale Then sets sale as cancelled")]
        public async Task Handle_ValidRequest_SetsSaleAsCancelled()
        {
            // Arrange
            var command = CreateUpdateSaleCommand(isCancelled: true);

            var sale = new Sale
            {
                Id = command.Id,
                SaleNumber = "123",
                Customer = "Customer A",
                Branch = "Branch 1"
            };

            _saleRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>())
                .Returns(Task.FromResult(sale));

            _saleRepository.UpdateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>())
                .Returns(Task.FromResult(sale));

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            sale.IsCancelled.Should().BeTrue();
        }

        // Helper method to generate a valid command (can be customized for your test cases)
        private static UpdateSaleCommand CreateUpdateSaleCommand(bool isCancelled = false)
        {
            var id = Guid.NewGuid();
            return new UpdateSaleCommand(id, "123", "Customer A", "Branch 1", isCancelled);
        }

    }

}
