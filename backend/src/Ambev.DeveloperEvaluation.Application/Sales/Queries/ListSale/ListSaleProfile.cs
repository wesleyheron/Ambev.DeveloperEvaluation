using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.Queries.ListSale
{
    /// <summary>
    /// Automapper profile for mapping between Sale entity and ListSalesResult DTO.
    /// </summary>
    public class ListSaleProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of ListSaleProfile for setting up object mappings.
        /// </summary>
        public ListSaleProfile()
        {
            CreateMap<Sale, ListSalesResult>()
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items))
                .ReverseMap();
        }
    }
}
