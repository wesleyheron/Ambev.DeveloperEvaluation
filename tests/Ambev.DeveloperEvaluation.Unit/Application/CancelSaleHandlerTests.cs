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
    public class CancelSaleHandlerTests
    {
        private readonly CancelSaleHandler _handler;
        private readonly ISaleRepository _saleRepository;
        private readonly SaleCancelledEventHandler _saleCancelledEventHandler;
        private readonly IEventProducer _mockEventProducer;

        public CancelSaleHandlerTests()
        {
            _saleRepository = Substitute.For<ISaleRepository>();
            _mockEventProducer = Substitute.For<IEventProducer>();
            _saleCancelledEventHandler = new SaleCancelledEventHandler(_mockEventProducer);
            _handler = new CancelSaleHandler(_saleRepository, _saleCancelledEventHandler);
        }

        [Fact(DisplayName = "Given invalid sale ID When cancelling sale Then throws KeyNotFoundException")]
        public async Task Handle_InvalidSaleId_ThrowsKeyNotFoundException()
        {
            // Arrange
            var command = new CancelSaleCommand { SaleId = Guid.NewGuid() };

            _saleRepository.GetByIdAsync(command.SaleId, Arg.Any<CancellationToken>())
                .Returns(Task.FromResult<Sale>(null));

            // Act
            var act = () => _handler.Handle(command, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<KeyNotFoundException>()
                .WithMessage($"Sale with ID {command.SaleId} not found.");
        }

        [Fact(DisplayName = "Given already cancelled sale When cancelling sale Then throws InvalidOperationException")]
        public async Task Handle_AlreadyCancelledSale_ThrowsInvalidOperationException()
        {
            // Arrange
            var command = new CancelSaleCommand { SaleId = Guid.NewGuid() };

            var sale = new Sale
            {
                Id = command.SaleId
            };

            sale.CancelSale(true);

            _saleRepository.GetByIdAsync(command.SaleId, Arg.Any<CancellationToken>())
                .Returns(Task.FromResult(sale));

            // Act
            var act = () => _handler.Handle(command, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<InvalidOperationException>()
                .WithMessage("Sale is already cancelled.");
        }

        [Fact(DisplayName = "Given valid sale data When cancelling sale Then returns true and updates the sale")]
        public async Task Handle_ValidSale_ReturnsTrueAndCancelsSale()
        {
            // Arrange
            var command = new CancelSaleCommand { SaleId = Guid.NewGuid() };

            var sale = new Sale
            {
                Id = command.SaleId
            };

            _saleRepository.GetByIdAsync(command.SaleId, Arg.Any<CancellationToken>())
                .Returns(Task.FromResult(sale));

            _saleRepository.UpdateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>())
                .Returns(Task.FromResult(sale));         

            var mockEventProducer = new Mock<IEventProducer>();
            var mockEventHandler = new SaleCancelledEventHandler(mockEventProducer.Object);
            var handler = new CancelSaleHandler(_saleRepository, mockEventHandler);
            mockEventProducer.Setup(ep => ep.PublishAsync(It.IsAny<SaleCancelledEvent>(), It.IsAny<string>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().BeTrue();

            await _saleRepository.Received(1).UpdateAsync(sale, Arg.Any<CancellationToken>());
            mockEventProducer.Verify(ep => ep.PublishAsync(It.IsAny<SaleCancelledEvent>(), "SaleCancelledEvent"), Times.Once);
        }
    }

}
