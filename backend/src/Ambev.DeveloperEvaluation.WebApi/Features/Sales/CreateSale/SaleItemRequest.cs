namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale
{
    /// <summary>
    /// Represents a request model for a sale item, including the product, quantity, and unit price.
    /// </summary>
    public class SaleItemRequest
    {
        /// <summary>
        /// The name or identifier of the product in the sale item.
        /// </summary>
        public string Product { get; set; } = string.Empty;

        /// <summary>
        /// The quantity of the product being sold.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// The unit price of the product in the sale.
        /// </summary>
        public decimal UnitPrice { get; set; }
    }
}
