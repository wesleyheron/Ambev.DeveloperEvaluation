using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData
{
    /// <summary>
    /// Provides methods for generating test data for the Sale entity.
    /// </summary>
    public static class SaleTestData
    {
        private static readonly Faker<Sale> SaleFaker = new Faker<Sale>()
            .CustomInstantiator(_ => new Sale())
            .RuleFor(s => s.SaleNumber, f => f.Commerce.Ean13())
            .RuleFor(s => s.Customer, _ => Guid.NewGuid().ToString())
            .RuleFor(s => s.Branch, f => f.Commerce.Department())
            .RuleFor(s => s.TotalAmount, _ => 0m)
            .RuleFor(s => s.IsCancelled, _ => false);

        private static readonly Faker<SaleItem> SaleItemFaker = new Faker<SaleItem>()
            .CustomInstantiator(f => new SaleItem()
            {
                Quantity = f.Random.Int(1, 20),
                UnitPrice = f.Finance.Amount(10, 100),
                Product = f.Commerce.ProductName(),
                SaleId = Guid.NewGuid()
            });

        /// <summary>
        /// Generates a valid Sale with a random number of items.
        /// </summary>
        /// <param name="itemCount">Number of items to generate.</param>
        /// <returns>A valid Sale entity with items.</returns>
        public static Sale GenerateValidSale(int itemCount = 3)
        {
            var sale = SaleFaker.Generate();
            sale.Id = Guid.NewGuid();
            sale.CreateSaleDate();

            var items = SaleItemFaker.Generate(itemCount);

            foreach (var item in items)
            {
                item.Id = Guid.NewGuid();
                item.SaleId = sale.Id;
                sale.AddItem(item);
            }

            return sale;
        }

        /// <summary>
        /// Generates a SaleItem with a specific quantity.
        /// </summary>
        /// <param name="quantity">The quantity of the SaleItem.</param>
        /// <returns>A SaleItem entity.</returns>
        public static SaleItem GenerateSaleItem(int quantity)
        {
            return SaleItemFaker.Clone()
                .RuleFor(i => i.Quantity, quantity)
                .Generate();
        }
    }
}
