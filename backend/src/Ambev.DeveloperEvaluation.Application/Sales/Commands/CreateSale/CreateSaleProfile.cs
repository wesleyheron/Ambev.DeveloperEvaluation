using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.Commands.CreateSale
{
    /// <summary>
    /// AutoMapper profile that defines mappings between the CreateSaleCommand, Sale, SaleItem, and CreateSaleResult types.
    /// </summary>
    public class CreateSaleProfile : Profile
    {
        public CreateSaleProfile()
        {
            /// <summary>
            /// Maps CreateSaleCommand to Sale, including mapping of sale items.
            /// </summary>
            CreateMap<CreateSaleCommand, Sale>()
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items))
                .ReverseMap();

            /// <summary>
            /// Maps SaleItemCommand to SaleItem.
            /// </summary>
            CreateMap<SaleItemCommand, SaleItem>().ReverseMap();

            /// <summary>
            /// Maps Sale to CreateSaleResult.
            /// </summary>
            CreateMap<Sale, CreateSaleResult>().ReverseMap();
        }
    }

}
