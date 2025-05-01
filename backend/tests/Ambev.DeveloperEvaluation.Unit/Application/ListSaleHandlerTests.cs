using Ambev.DeveloperEvaluation.Application.Sales.Queries.ListSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application
{
    public class ListSaleHandlerTests
    {
        [Fact(DisplayName = "Given sales data When handling Then returns correct sales list")]
        public async Task Handle_ValidRequest_ReturnsCorrectSalesList()
        {
            // Arrange
            var saleId1 = Guid.NewGuid();
            var saleId2 = Guid.NewGuid();
            var sale1 = new Sale
            {
                Id = saleId1,
                SaleNumber = "S1234",
                SaleDate = DateTime.Now,
                Customer = "Customer A",
                Branch = "Branch X",
                Items =
                [
                    new SaleItem { Id = Guid.NewGuid(), Product = "Product 1", Quantity = 2, UnitPrice = 10, SaleId = saleId1 },
                    new SaleItem { Id = Guid.NewGuid(), Product = "Product 2", Quantity = 1, UnitPrice = 15, SaleId = saleId1 }
                ]
            };

            var sale2 = new Sale
            {
                Id = saleId2,
                SaleNumber = "S5678",
                SaleDate = DateTime.Now.AddDays(-1),
                Customer = "Customer B",
                Branch = "Branch Y",
                Items =
                [
                    new SaleItem { Id = Guid.NewGuid(), Product = "Product 3", Quantity = 1, UnitPrice = 20, SaleId = saleId2 }
                ]
            };

            sale1.CalculateTotalAmount();
            sale2.CalculateTotalAmount();
            sale2.CancelSale(true);

            var salesRepositoryMock = Substitute.For<ISaleRepository>();
            salesRepositoryMock.GetAllAsync(Arg.Any<CancellationToken>())
                   .Returns(Task.FromResult((IEnumerable<Sale>)[sale1, sale2]));


            var handler = new ListSaleHandler(salesRepositoryMock);
            var query = new ListSaleQuery();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);

            var firstSale = result.First();
            firstSale.SaleNumber.Should().Be("S1234");
            firstSale.TotalAmount.Should().Be(35);
            firstSale.IsCancelled.Should().BeFalse();
            firstSale.Items.Should().HaveCount(2);
            firstSale.Items.First().Product.Should().Be("Product 1");

            var secondSale = result.Last();
            secondSale.SaleNumber.Should().Be("S5678");
            secondSale.TotalAmount.Should().Be(20);
            secondSale.IsCancelled.Should().BeTrue();
            secondSale.Items.Should().HaveCount(1);
            secondSale.Items.First().Product.Should().Be("Product 3");
        }

        [Fact(DisplayName = "Given no sales data When handling Then returns an empty list")]
        public async Task Handle_NoSalesData_ReturnsEmptyList()
        {
            // Arrange
            var salesRepositoryMock = Substitute.For<ISaleRepository>();
            salesRepositoryMock.GetAllAsync(Arg.Any<CancellationToken>())
                               .Returns(Task.FromResult((IEnumerable<Sale>)[]));

            var handler = new ListSaleHandler(salesRepositoryMock);
            var query = new ListSaleQuery();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEmpty();
        }
    }
}