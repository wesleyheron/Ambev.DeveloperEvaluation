using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.Commands.DeleteSale
{
    /// <summary>
    /// Represents the command to delete a sale in the system.
    /// This command contains all the necessary information to delete a sale.
    /// </summary>
    public class DeleteSaleCommand : IRequest<DeleteSaleResult>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the sale to delete.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Initializes a new instance of the DeleteSaleCommand class.
        /// </summary>
        /// <param name="saleId">The ID of the sale to delete.</param>
        public DeleteSaleCommand(Guid saleId)
        {
            Id = saleId;
        }
    }
}
