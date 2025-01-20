namespace Ambev.DeveloperEvaluation.Application.Sales.Commands.CreateSale
{
    /// <summary>
    /// Represents the result of a sale item, including its details such as product, quantity, 
    /// unit price, discount, total amount, cancellation status, and the sale it belongs to.
    /// </summary>
    public class SaleItemResult
    {
        /// <summary>
        /// Gets or sets the unique identifier of the sale item.
        /// </summary>
        /// <value>The unique identifier (GUID) of the sale item.</value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the product for the sale item.
        /// </summary>
        /// <value>The name of the product associated with the sale item.</value>
        public string Product { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the quantity of the product sold.
        /// </summary>
        /// <value>The number of units of the product sold in this sale item.</value>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the unit price of the product.
        /// </summary>
        /// <value>The price per individual unit of the product.</value>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// Gets or sets the discount applied to the product.
        /// </summary>
        /// <value>The discount applied to the item, if any.</value>
        public decimal Discount { get; set; }

        /// <summary>
        /// Gets or sets the total amount for this sale item.
        /// </summary>
        /// <value>The total value of this sale item, considering quantity, unit price, and discount.</value>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// Gets or sets whether the sale item has been cancelled.
        /// </summary>
        /// <value>A boolean indicating whether the sale item is cancelled.</value>
        public bool IsCancelled { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the sale to which this item belongs.
        /// </summary>
        /// <value>The unique identifier (GUID) of the sale associated with this sale item.</value>
        public Guid SaleId { get; set; }
    }
}
