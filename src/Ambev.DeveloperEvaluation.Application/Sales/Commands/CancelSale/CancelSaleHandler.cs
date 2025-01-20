using Ambev.DeveloperEvaluation.Application.Sales.Events;
using Ambev.DeveloperEvaluation.Domain.Events.Sales;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.Commands.CancelSale
{
    /// <summary>
    /// Handles the logic for canceling a sale in the system.
    /// This handler processes the <see cref="CancelSaleCommand"/> and updates the sale status to canceled.
    /// It also triggers the <see cref="SaleCancelledEvent"/> to notify other components of the cancellation.
    /// </summary>
    public class CancelSaleHandler : IRequestHandler<CancelSaleCommand, bool>
    {
        /// <summary>
        /// The repository used for persisting and retrieving sale data.
        /// </summary>
        private readonly ISaleRepository _saleRepository;

        /// <summary>
        /// The event handler responsible for handling the sale cancelled event.
        /// This handler publishes notifications when a sale is cancelled successfully.
        /// </summary>
        private readonly SaleCancelledEventHandler _saleCancelledEventHandler;

        /// <summary>
        /// Initializes a new instance of the <see cref="CancelSaleHandler"/> class.
        /// </summary>
        /// <param name="saleRepository">The repository used to retrieve and update sale data.</param>
        /// <param name="saleCancelledEventHandler">The event handler that processes sale cancellation events.</param>
        public CancelSaleHandler(ISaleRepository saleRepository, SaleCancelledEventHandler saleCancelledEventHandler)
        {
            _saleRepository = saleRepository;
            _saleCancelledEventHandler = saleCancelledEventHandler;
        }

        /// <summary>
        /// Handles the cancellation of a sale based on the given command.
        /// Updates the sale status to canceled and triggers the sale cancellation event.
        /// </summary>
        /// <param name="request">The command containing the ID of the sale to cancel.</param>
        /// <param name="cancellationToken">A token to monitor the cancellation of the operation.</param>
        /// <returns>Returns true if the cancellation was successful, otherwise throws an exception.</returns>
        /// <exception cref="KeyNotFoundException">Thrown if the sale with the provided ID does not exist.</exception>
        /// <exception cref="InvalidOperationException">Thrown if the sale is already canceled.</exception>
        public async Task<bool> Handle(CancelSaleCommand request, CancellationToken cancellationToken)
        {
            var sale = await _saleRepository.GetByIdAsync(request.SaleId, cancellationToken)
                   ?? throw new KeyNotFoundException($"Sale with ID {request.SaleId} not found.");

            if (sale.IsCancelled)
                throw new InvalidOperationException("Sale is already cancelled.");

            sale.CancelSale(true);

            await _saleRepository.UpdateAsync(sale, cancellationToken);

            await _saleCancelledEventHandler.Handle(new SaleCancelledEvent(sale), cancellationToken);

            return true;
        }
    }
}
