using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.Commands.UpdateSale
{
    /// <summary>
    /// Automapper profile configuration for mapping between <see cref="UpdateSaleCommand"/>, <see cref="Sale"/>, and <see cref="UpdateSaleResult"/>.
    /// </summary>
    public class UpdateSaleProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateSaleProfile"/> class.
        /// Configures the object mappings for updating sales data.
        /// </summary>
        public UpdateSaleProfile()
        {
            // Map UpdateSaleCommand to Sale, including mapping of Sale Items
            CreateMap<UpdateSaleCommand, Sale>()
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items))
                .ReverseMap();

            // Map Sale to UpdateSaleResult and vice versa
            CreateMap<Sale, UpdateSaleResult>().ReverseMap();
        }
    }
}
