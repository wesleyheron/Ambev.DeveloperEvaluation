using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.Queries.ListSale
{
    /// <summary>
    /// Represents a query to list sales.
    /// </summary>
    public class ListSaleQuery : IRequest<List<ListSalesResult>>
    {
        /// <summary>
        /// Initializes a new instance of the ListSaleQuery class.
        /// </summary>
        public ListSaleQuery()
        {
        }
    }
}
