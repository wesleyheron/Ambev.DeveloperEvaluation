using Ambev.DeveloperEvaluation.Application.Sales.Commands.DeleteSale;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.DeleteSale
{
    /// <summary>
    /// AutoMapper profile class to map DeleteSaleRequest to DeleteSaleCommand.
    /// </summary>
    public class DeleteSaleProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the DeleteSaleProfile and defines the mapping between DeleteSaleRequest and DeleteSaleCommand.
        /// </summary>
        public DeleteSaleProfile()
        {
            CreateMap<DeleteSaleRequest, DeleteSaleCommand>();
        }
    }
}
