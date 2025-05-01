namespace Ambev.DeveloperEvaluation.Application.Sales.Queries.GetSale
{
    /// <summary>
    /// Represents the result details of a sale item.
    /// </summary>
    public class GetSaleItemResult
    {
        /// <summary>
        /// Gets or sets the Id of the item.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the product name of the item.
        /// </summary>
        public string Product { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the quantity of the item.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the unit price of the item.
        /// </summary>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// Gets or sets the discount applied to the item.
        /// </summary>
        public decimal Discount { get; set; }

        /// <summary>
        /// Gets or sets the total amount for the item.
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// Gets or sets the Sale Id of the item.
        /// </summary>
        public Guid SaleId { get; set; }
    }
}
