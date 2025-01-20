using Ambev.DeveloperEvaluation.Application.Sales.Queries.ListSale;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.ListSale
{
    /// <summary>
    /// AutoMapper profile to define the mappings between request, query, result, and response for sale-related operations.
    /// </summary>
    public class ListSaleProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the ListSaleProfile with predefined mappings.
        /// </summary>
        public ListSaleProfile()
        {
            CreateMap<ListSaleQuery, ListSalesRequest>();
            CreateMap<ListSalesResult, ListSaleResult>();
            CreateMap<ListSaleItemResult, ListSaleItemResult>();
        }
    }
}
