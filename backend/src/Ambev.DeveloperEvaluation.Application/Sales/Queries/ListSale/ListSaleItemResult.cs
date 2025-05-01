namespace Ambev.DeveloperEvaluation.Application.Sales.Queries.ListSale
{
    /// <summary>
    /// Represents the details of an item in a sale, including its characteristics and cancellation status.
    /// </summary>
    public class ListSaleItemResult
    {
        /// <summary>
        /// Unique identifier of the sale item.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The product name.
        /// </summary>
        public string Product { get; set; } = string.Empty;

        /// <summary>
        /// Quantity of the product sold.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Unit price of the product.
        /// </summary>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// Discount applied to the product.
        /// </summary>
        public decimal Discount { get; set; }

        /// <summary>
        /// Total amount for this sale item.
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// Indicates whether the item is cancelled.
        /// </summary>
        public bool IsCancelled { get; set; }

        /// <summary>
        /// The ID of the sale to which this item belongs.
        /// </summary>
        public Guid SaleId { get; set; }
    }
}
