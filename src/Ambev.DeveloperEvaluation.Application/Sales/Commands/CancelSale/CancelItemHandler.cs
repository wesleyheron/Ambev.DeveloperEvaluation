using Ambev.DeveloperEvaluation.Application.Sales.Events;
using Ambev.DeveloperEvaluation.Domain.Events.Sales;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.Commands.CancelSale
{
    /// <summary>
    /// Handles the cancellation of a specific item within a sale. 
    /// Retrieves the sale and item, applies the cancellation logic, 
    /// and updates the sale repository accordingly. Additionally, 
    /// triggers the event for item cancellation.
    /// </summary>
    public class CancelItemHandler : IRequestHandler<CancelItemCommand, bool>
    {
        /// <summary>
        /// The repository used for persisting and retrieving sale data.
        /// </summary>
        private readonly ISaleRepository _saleRepository;

        /// <summary>
        /// The event handler responsible for handling the sale item cancelled event.
        /// This handler publishes notifications when a sale item is cancelled successfully.
        /// </summary>
        private readonly SaleItemCancelledEventHandler _saleItemCancelledEventHandler;

        /// <summary>
        /// Initializes a new instance of the <see cref="CancelItemHandler"/> class.
        /// </summary>
        /// <param name="saleRepository">The repository responsible for handling sale data operations.</param>
        /// <param name="saleItemCancelledEventHandler">Handler for triggering events related to item cancellation.</param>
        public CancelItemHandler(ISaleRepository saleRepository, SaleItemCancelledEventHandler saleItemCancelledEventHandler)
        {
            _saleRepository = saleRepository;
            _saleItemCancelledEventHandler = saleItemCancelledEventHandler;
        }

        /// <summary>
        /// Handles the cancellation of an item in the sale.
        /// Retrieves the sale and item based on their IDs, cancels the item, 
        /// updates the sale in the repository, and triggers the cancellation event.
        /// </summary>
        /// <param name="request">The command containing the sale and item IDs to cancel.</param>
        /// <param name="cancellationToken">The cancellation token to observe while processing the request.</param>
        /// <returns>Returns <c>true</c> if the item cancellation was successful.</returns>
        /// <exception cref="KeyNotFoundException">
        /// Thrown if the sale or item cannot be found in the repository.
        /// </exception>
        public async Task<bool> Handle(CancelItemCommand request, CancellationToken cancellationToken)
        {
            var sale = await _saleRepository.GetByIdAsync(request.SaleId, cancellationToken)
                   ?? throw new KeyNotFoundException($"Sale with ID {request.SaleId} not found.");
            var item = sale.Items.FirstOrDefault(i => i.Id == request.ItemId)
                       ?? throw new KeyNotFoundException($"Item with ID {request.ItemId} not found in sale.");

            sale.CancelItem(request.ItemId);

            await _saleRepository.UpdateAsync(sale, cancellationToken);

            await _saleItemCancelledEventHandler.Handle(new ItemCancelledEvent(sale, item), cancellationToken);

            return true;
        }
    }
}
