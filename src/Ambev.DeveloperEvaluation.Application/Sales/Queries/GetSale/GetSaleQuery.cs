using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.Queries.GetSale
{
    /// <summary>
    /// Represents the query to retrieve a sale by its identifier.
    /// </summary>
    public class GetSaleQuery : IRequest<GetSaleResult>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the sale.
        /// </summary>
        public Guid SaleId { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetSaleQuery"/> class.
        /// </summary>
        /// <param name="saleId">The unique identifier of the sale to be retrieved.</param>
        public GetSaleQuery(Guid saleId)
        {
            SaleId = saleId;
        }
    }
}
