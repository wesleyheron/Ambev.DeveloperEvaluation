using Ambev.DeveloperEvaluation.Application.Sales.Queries.GetSale;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale
{
    /// <summary>
    /// AutoMapper profile for mapping GetSale-related request and response models.
    /// </summary>
    public class GetSaleProfile : Profile
    {
        /// <summary>
        /// Initializes mappings for GetSale-related requests and responses.
        /// </summary>
        public GetSaleProfile()
        {
            CreateMap<GetSaleRequest, GetSaleQuery>();
            CreateMap<GetSaleResult, GetSaleResult>();
            CreateMap<GetSaleItemResult, SaleItemResponse>();
        }
    }
}
