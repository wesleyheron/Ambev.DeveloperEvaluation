using Ambev.DeveloperEvaluation.Application.Sales.Commands.CancelSale;
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
    public class CancelItemHandlerTests
    {
        private readonly CancelItemHandler _handler;
        private readonly ISaleRepository _saleRepository;
        private readonly SaleItemCancelledEventHandler _saleItemCancelledEventHandler;
        private readonly IEventProducer _mockEventProducer;

        public CancelItemHandlerTests()
        {
            _saleRepository = Substitute.For<ISaleRepository>();
            _mockEventProducer = Substitute.For<IEventProducer>();
            _saleItemCancelledEventHandler = new SaleItemCancelledEventHandler(_mockEventProducer);
            _handler = new CancelItemHandler(_saleRepository, _saleItemCancelledEventHandler);
        }

        [Fact(DisplayName = "Given invalid sale ID When cancelling item Then throws KeyNotFoundException")]
        public async Task Handle_InvalidSaleId_ThrowsKeyNotFoundException()
        {
            // Arrange
            var command = new CancelItemCommand { SaleId = Guid.NewGuid(), ItemId = Guid.NewGuid() };

            _saleRepository.GetByIdAsync(command.SaleId, Arg.Any<CancellationToken>())
                .Returns(Task.FromResult<Sale>(null));

            // Act
            var act = () => _handler.Handle(command, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<KeyNotFoundException>()
                .WithMessage($"Sale with ID {command.SaleId} not found.");
        }

        [Fact(DisplayName = "Given invalid item ID When cancelling item Then throws KeyNotFoundException")]
        public async Task Handle_InvalidItemId_ThrowsKeyNotFoundException()
        {
            // Arrange
            var command = new CancelItemCommand { SaleId = Guid.NewGuid(), ItemId = Guid.NewGuid() };

            var sale = new Sale
            {
                Id = command.SaleId,
                Items = []
            };

            _saleRepository.GetByIdAsync(command.SaleId, Arg.Any<CancellationToken>())
                .Returns(Task.FromResult(sale));

            // Act
            var act = () => _handler.Handle(command, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<KeyNotFoundException>()
                .WithMessage($"Item with ID {command.ItemId} not found in sale.");
        }

        [Fact(DisplayName = "Given valid sale and item data When cancelling item Then returns true and updates sale")]
        public async Task Handle_ValidSaleAndItem_ReturnsTrueAndCancelsItem()
        {
            // Arrange
            var command = new CancelItemCommand { SaleId = Guid.NewGuid(), ItemId = Guid.NewGuid() };

            var sale = new Sale
            {
                Id = command.SaleId,
                Items =
                [
                    new SaleItem { Id = command.ItemId, Product = "Product A", Quantity = 1, UnitPrice = 100 }
                ]
            };

            _saleRepository.GetByIdAsync(command.SaleId, Arg.Any<CancellationToken>())
                .Returns(Task.FromResult(sale));

            _saleRepository.UpdateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>())
                .Returns(Task.FromResult(sale));            

            var mockEventProducer = new Mock<IEventProducer>();
            var mockEventHandler = new SaleItemCancelledEventHandler(mockEventProducer.Object);
            var handler = new CancelItemHandler(_saleRepository, mockEventHandler);
            mockEventProducer.Setup(ep => ep.PublishAsync(It.IsAny<ItemCancelledEvent>(), It.IsAny<string>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().BeTrue();

            await _saleRepository.Received(1).UpdateAsync(sale, Arg.Any<CancellationToken>());
            mockEventProducer.Verify(ep => ep.PublishAsync(It.IsAny<ItemCancelledEvent>(), "ItemCancelledEvent"), Times.Once);
        }
    }

}
