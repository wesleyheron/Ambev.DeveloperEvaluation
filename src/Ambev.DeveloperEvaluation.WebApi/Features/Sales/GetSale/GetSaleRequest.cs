namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale
{
    /// <summary>
    /// Represents a request to retrieve a sale by its unique identifier.
    /// </summary>
    public class GetSaleRequest
    {
        /// <summary>
        /// The unique identifier of the sale to retrieve.
        /// </summary>
        public Guid SaleId { get; set; }
    }

}
