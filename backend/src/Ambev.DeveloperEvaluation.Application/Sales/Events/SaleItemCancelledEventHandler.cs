using Ambev.DeveloperEvaluation.Domain.Events.Sales;
using Ambev.DeveloperEvaluation.MessageBroker.Abstractions;

namespace Ambev.DeveloperEvaluation.Application.Sales.Events
{
    /// <summary>
    /// Event handler for handling the cancellation of a sale item. 
    /// Inherits from a generic EventHandler to process ItemCancelledEvent notifications.
    /// </summary>
    public class SaleItemCancelledEventHandler : EventHandler<ItemCancelledEvent>
    {
        private readonly IEventProducer _eventProducer;

        /// <summary>
        /// Initializes a new instance of the SaleItemCancelledEventHandler class.
        /// </summary>
        /// <param name="eventProducer">The event producer responsible for triggering events.</param>
        public SaleItemCancelledEventHandler(IEventProducer eventProducer)
            : base(eventProducer)
        {
            _eventProducer = eventProducer;
        }

        /// <summary>
        /// Handles the ItemCancelledEvent notification when a sale item is cancelled.
        /// </summary>
        /// <param name="notification">The ItemCancelledEvent notification to handle.</param>
        /// <param name="cancellationToken">A cancellation token to observe while handling the event.</param>
        /// <returns>A task representing the asynchronous event handling operation.</returns>
        public override async Task Handle(ItemCancelledEvent notification, CancellationToken cancellationToken)
        {
            await _eventProducer.PublishAsync(notification, typeof(ItemCancelledEvent).Name);
        }
    }

}
