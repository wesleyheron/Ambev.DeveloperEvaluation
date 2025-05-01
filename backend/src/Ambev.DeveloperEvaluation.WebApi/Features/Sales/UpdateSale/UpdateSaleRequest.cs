using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale
{
    /// <summary>
    /// Represents a request to update an existing sale, including its sale number, customer, branch, cancellation status, 
    /// and the list of sale items. The sale's identifier and other required fields must be provided.
    /// </summary>
    public class UpdateSaleRequest
    {
        /// <summary>
        /// The unique identifier of the sale.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The unique sale number.
        /// </summary>
        public required string SaleNumber { get; set; }

        /// <summary>
        /// The name of the customer associated with the sale.
        /// </summary>
        public required string Customer { get; set; }

        /// <summary>
        /// The branch where the sale occurred.
        /// </summary>
        public required string Branch { get; set; }

        /// <summary>
        /// Indicates whether the sale is cancelled.
        /// </summary>
        public bool IsCancelled { get; set; }

        /// <summary>
        /// The list of items in the sale that may be updated.
        /// </summary>
        public List<SaleItemRequest> Items { get; set; } = [];
    }

}
