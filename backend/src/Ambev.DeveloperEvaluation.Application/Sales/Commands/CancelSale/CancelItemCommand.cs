using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.Commands.CancelSale
{
    /// <summary>
    /// Represents a command to cancel a specific item in a sale transaction.
    /// Contains the necessary identifiers to locate the sale and item to be cancelled.
    /// </summary>
    public class CancelItemCommand : IRequest<bool>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the sale that contains the item to be cancelled.
        /// </summary>
        public Guid SaleId { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the item to be cancelled in the sale.
        /// </summary>
        public Guid ItemId { get; set; }
    }
}
