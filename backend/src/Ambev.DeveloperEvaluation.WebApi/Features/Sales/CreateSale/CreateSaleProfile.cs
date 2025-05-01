using Ambev.DeveloperEvaluation.Application.Sales.Commands.CreateSale;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale
{
    /// <summary>
    /// AutoMapper profile class that defines mappings between the request, command, result, and response objects for the sale creation process.
    /// </summary>
    public class CreateSaleProfile : Profile
    {
        public CreateSaleProfile()
        {
            // Map CreateSaleRequest to CreateSaleCommand and vice versa
            CreateMap<CreateSaleRequest, CreateSaleCommand>().ReverseMap();

            // Map CreateSaleResult to CreateSaleResponse and vice versa
            CreateMap<CreateSaleResult, CreateSaleResponse>().ReverseMap();
        }
    }
}
