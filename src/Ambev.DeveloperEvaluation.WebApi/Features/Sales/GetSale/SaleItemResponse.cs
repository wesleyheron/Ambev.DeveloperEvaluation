namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale
{
    /// <summary>
    /// Represents the details of a sale item.
    /// </summary>
    public class SaleItemResponse
    {
        /// <summary>
        /// The unique identifier of the sale item.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The product name.
        /// </summary>
        public string Product { get; set; } = string.Empty;

        /// <summary>
        /// The quantity of the product.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// The unit price of the product.
        /// </summary>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// The discount applied to the product.
        /// </summary>
        public decimal Discount { get; set; }

        /// <summary>
        /// The total amount for the product.
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// The unique identifier of the sale to which this item belongs.
        /// </summary>
        public Guid SaleId { get; set; }
    }
}
