namespace Ambev.DeveloperEvaluation.Application.Sales.Queries.GetSale
{
    /// <summary>
    /// Represents the result of retrieving a sale, containing all necessary details about the sale.
    /// </summary>
    public class GetSaleResult
    {
        /// <summary>
        /// Gets or sets the unique identifier of the sale.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the sale number.
        /// </summary>
        public string SaleNumber { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the date when the sale occurred.
        /// </summary>
        public DateTime SaleDate { get; set; }

        /// <summary>
        /// Gets or sets the name of the customer associated with the sale.
        /// </summary>
        public string Customer { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the branch where the sale occurred.
        /// </summary>
        public string Branch { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the list of items in the sale.
        /// </summary>
        public List<GetSaleItemResult> Items { get; set; } = new();
    }
}
