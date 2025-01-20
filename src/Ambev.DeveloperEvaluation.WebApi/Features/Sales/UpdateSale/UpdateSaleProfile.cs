using Ambev.DeveloperEvaluation.Application.Sales.Commands.UpdateSale;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale
{
    /// <summary>
    /// Profile configuration for mapping between UpdateSaleRequest, UpdateSaleCommand, 
    /// and their reverse mappings, as well as between UpdateSaleResult and UpdateSaleResponse.
    /// </summary>
    public class UpdateSaleProfile : Profile
    {
        public UpdateSaleProfile()
        {
            CreateMap<UpdateSaleRequest, UpdateSaleCommand>().ReverseMap();
            CreateMap<UpdateSaleResult, UpdateSaleResponse>().ReverseMap();
        }
    }
}
