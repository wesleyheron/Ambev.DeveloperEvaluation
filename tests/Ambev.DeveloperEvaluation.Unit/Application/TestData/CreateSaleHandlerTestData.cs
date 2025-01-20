using Ambev.DeveloperEvaluation.Application.Sales.Commands.CreateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData
{
    public static class CreateSaleHandlerTestData
    {
        public static CreateSaleCommand GenerateValidCommand()
        {
            return new CreateSaleCommand
            {
                SaleNumber = "SALE123",
                Customer = "John Doe",
                Branch = "Branch A",
                Items =
            [
                new SaleItemCommand
                {
                    Product = "Product A",
                    Quantity = 2,
                    UnitPrice = 50
                },
                new SaleItemCommand
                {
                    Product = "Product B",
                    Quantity = 1,
                    UnitPrice = 100
                }
            ]
            };
        }

        public static Sale GenerateValidSale(CreateSaleCommand command)
        {
            return new Sale
            {
                Id = Guid.NewGuid(),
                SaleNumber = command.SaleNumber,
                Customer = command.Customer,
                Branch = command.Branch,
                SaleDate = DateTime.UtcNow,
                Items = command.Items.Select(i => new SaleItem
                {
                    Id = Guid.NewGuid(),
                    Product = i.Product,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice
                }).ToList(),
            };
        }

        public static CreateSaleResult GenerateCreateSaleResult(Sale sale)
        {
            return new CreateSaleResult
            {
                Id = sale.Id,
                SaleNumber = sale.SaleNumber,
                Customer = sale.Customer,
                Branch = sale.Branch,
                SaleDate = sale.SaleDate,
                TotalAmount = sale.TotalAmount,
                Items = sale.Items.Select(i => new SaleItemResult
                {
                    Id = i.Id,
                    Product = i.Product,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice,
                    TotalAmount = i.TotalAmount,
                    Discount = i.Discount
                }).ToList()
            };
        }
    }

}
