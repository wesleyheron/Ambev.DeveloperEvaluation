using Ambev.DeveloperEvaluation.Application.Sales.Queries.GetSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application
{
    public class GetSaleHandlerTests
    {
        [Fact(DisplayName = "Given valid sale ID When handling Then returns correct sale details")]
        public async Task Handle_ValidSaleId_ReturnsCorrectSaleDetails()
        {
            // Arrange
            var saleId = Guid.NewGuid();
            var sale = new Sale
            {
                Id = saleId,
                SaleNumber = "S1234",
                SaleDate = DateTime.Now,
                Customer = "Customer A",
                Branch = "Branch X",
                Items =
                [
                    new SaleItem { Id = Guid.NewGuid(), Product = "Product 1", Quantity = 2, UnitPrice = 10, SaleId = saleId },
                    new SaleItem { Id = Guid.NewGuid(), Product = "Product 2", Quantity = 1, UnitPrice = 15, SaleId = saleId }
                ]
            };

            foreach (var item in sale.Items)
            {
                item.ApplyDiscount();
            }

            var saleRepositoryMock = Substitute.For<ISaleRepository>();
            saleRepositoryMock.GetByIdAsync(saleId, Arg.Any<CancellationToken>())
                              .Returns(Task.FromResult(sale));

            var handler = new GetSaleHandler(saleRepositoryMock);
            var query = new GetSaleQuery(saleId);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(saleId);
            result.SaleNumber.Should().Be("S1234");
            result.Items.Should().HaveCount(2);
            result.Items[0].Product.Should().Be("Product 1");
            result.Items[0].TotalAmount.Should().Be(2 * 10);
            result.Items[1].TotalAmount.Should().Be(1 * 15);
        }

        [Fact(DisplayName = "Given invalid sale ID When handling Then throws KeyNotFoundException")]
        public async Task Handle_InvalidSaleId_ThrowsKeyNotFoundException()
        {
            // Arrange
            var saleId = Guid.NewGuid();
            var saleRepositoryMock = Substitute.For<ISaleRepository>();
            saleRepositoryMock.GetByIdAsync(saleId, Arg.Any<CancellationToken>())
                              .Returns(Task.FromResult<Sale>(null));

            var handler = new GetSaleHandler(saleRepositoryMock);
            var query = new GetSaleQuery(saleId);

            // Act
            var act = async () => await handler.Handle(query, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<KeyNotFoundException>()
                     .WithMessage($"Sale with ID {saleId} not found");
        }
    }
}
