namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.ListSale
{
    /// <summary>
    /// Represents the result of a sale, including sale details and associated sale items.
    /// </summary>
    public class ListSaleResult
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
        /// The date the sale was made.
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
        /// The list of items associated with the sale.
        /// </summary>
        public List<ListSaleItemResult> Items { get; set; } = [];
    }
}
