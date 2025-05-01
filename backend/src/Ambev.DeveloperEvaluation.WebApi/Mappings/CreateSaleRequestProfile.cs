using Ambev.DeveloperEvaluation.Application.Sales.Commands.CreateSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Mappings;

public class CreateSaleRequestProfile : Profile
{
    public CreateSaleRequestProfile()
    {
        CreateMap<CreateSaleRequest, CreateSaleCommand>();
        CreateMap<SaleItemRequest, SaleItemCommand>();

        CreateMap<CreateSaleResult, CreateSaleResponse>();
        CreateMap<SaleItemResult, SaleItemResponse>();
    }
}