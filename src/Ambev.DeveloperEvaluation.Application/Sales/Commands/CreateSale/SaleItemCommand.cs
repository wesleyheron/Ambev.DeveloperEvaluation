namespace Ambev.DeveloperEvaluation.Application.Sales.Commands.CreateSale
{
    /// <summary>
    /// Represents a command for creating or updating an item in a sale.
    /// Contains details about the product, quantity, and unit price of the item.
    /// </summary>
    public class SaleItemCommand
    {
        /// <summary>
        /// Gets or sets the name of the product for the item.
        /// </summary>
        /// <value>The name of the product associated with the item.</value>
        public string Product { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the quantity of the item in the sale.
        /// </summary>
        /// <value>The number of units of the item being sold.</value>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the unit price of the item.
        /// </summary>
        /// <value>The price per individual unit of the item.</value>
        public decimal UnitPrice { get; set; }
    }
}
