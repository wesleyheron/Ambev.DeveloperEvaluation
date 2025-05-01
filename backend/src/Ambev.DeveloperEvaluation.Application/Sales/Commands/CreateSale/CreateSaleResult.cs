namespace Ambev.DeveloperEvaluation.Application.Sales.Commands.CreateSale
{
    /// <summary>
    /// Represents the result of a created sale, including essential details such as sale information, customer, and sale items.
    /// </summary>
    public class CreateSaleResult
    {
        /// <summary>
        /// The unique identifier of the sale.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The sale number.
        /// </summary>
        public string SaleNumber { get; set; } = string.Empty;

        /// <summary>
        /// The date of the sale.
        /// </summary>
        public DateTime SaleDate { get; set; }

        /// <summary>
        /// The customer associated with the sale.
        /// </summary>
        public string Customer { get; set; } = string.Empty;

        /// <summary>
        /// The branch where the sale occurred.
        /// </summary>
        public string Branch { get; set; } = string.Empty;

        /// <summary>
        /// The total amount of the sale.
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// Indicates whether the sale is cancelled.
        /// </summary>
        public bool IsCancelled { get; set; }

        /// <summary>
        /// The list of items in the sale.
        /// </summary>
        public List<SaleItemResult> Items { get; set; } = new();
    }
}
