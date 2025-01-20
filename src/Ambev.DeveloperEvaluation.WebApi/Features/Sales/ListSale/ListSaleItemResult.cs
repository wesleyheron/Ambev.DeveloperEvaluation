namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.ListSale
{
    /// <summary>
    /// Represents a list item for a sale with details such as product, quantity, and price.
    /// </summary>
    public class ListSaleItemResult
    {
        /// <summary>
        /// The unique identifier of the sale item.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The product name associated with the sale item.
        /// </summary>
        public string Product { get; set; } = string.Empty;

        /// <summary>
        /// The quantity of the product in the sale.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// The unit price of the product in the sale.
        /// </summary>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// The discount applied to the product in the sale.
        /// </summary>
        public decimal Discount { get; set; }

        /// <summary>
        /// The total amount for the sale item (after applying discount).
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// Indicates whether the sale item is cancelled.
        /// </summary>
        public bool IsCancelled { get; set; }
    }

}
