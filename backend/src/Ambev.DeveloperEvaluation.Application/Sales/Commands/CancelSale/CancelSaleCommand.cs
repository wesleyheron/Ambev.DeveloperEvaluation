using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.Commands.CancelSale
{
    /// <summary>
    /// Represents a command to cancel a sale in the system.
    /// Contains the unique identifier of the sale that is to be canceled.
    /// </summary>
    public class CancelSaleCommand : IRequest<bool>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the sale to be canceled.
        /// </summary>
        public Guid SaleId { get; set; }
    }
}
