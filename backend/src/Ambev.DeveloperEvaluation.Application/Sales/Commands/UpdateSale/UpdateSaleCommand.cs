using Ambev.DeveloperEvaluation.Application.Sales.Commands.CreateSale;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.Commands.UpdateSale
{
    /// <summary>
    /// Represents a command to update an existing sale.
    /// </summary>
    public class UpdateSaleCommand : IRequest<UpdateSaleResult>
    {
        /// <summary>
        /// The unique identifier of the sale to update.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The updated sale number.
        /// </summary>
        public string SaleNumber { get; set; } = string.Empty;

        /// <summary>
        /// The updated customer name.
        /// </summary>
        public string Customer { get; set; } = string.Empty;

        /// <summary>
        /// The updated branch name.
        /// </summary>
        public string Branch { get; set; } = string.Empty;

        /// <summary>
        /// Indicates whether the sale is cancelled.
        /// </summary>
        public bool IsCancelled { get; set; }

        /// <summary>
        /// The list of items to be updated for the sale.
        /// </summary>
        public List<SaleItemCommand> Items { get; set; } = new List<SaleItemCommand>();

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateSaleCommand"/> class.
        /// </summary>
        /// <param name="id">The unique identifier of the sale.</param>
        /// <param name="saleNumber">The updated sale number.</param>
        /// <param name="customer">The updated customer name.</param>
        /// <param name="branch">The updated branch name.</param>
        /// <param name="isCancelled">Indicates whether the sale is cancelled.</param>
        public UpdateSaleCommand(Guid id, string saleNumber, string customer, string branch, bool isCancelled)
        {
            Id = id;
            SaleNumber = saleNumber;
            Customer = customer;
            Branch = branch;
            IsCancelled = isCancelled;
        }
    }
}
