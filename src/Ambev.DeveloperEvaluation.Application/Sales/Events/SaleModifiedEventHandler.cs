using Ambev.DeveloperEvaluation.Domain.Events.Sales;
using Ambev.DeveloperEvaluation.MessageBroker.Abstractions;

namespace Ambev.DeveloperEvaluation.Application.Sales.Events
{
    /// <summary>
    /// Event handler for handling the modification of a sale. 
    /// Inherits from a generic EventHandler to process SaleModifiedEvent notifications.
    /// </summary>
    public class SaleModifiedEventHandler : EventHandler<SaleModifiedEvent>
    {
        private readonly IEventProducer _eventProducer;
        /// <summary>
        /// Initializes a new instance of the SaleModifiedEventHandler class.
        /// </summary>
        /// <param name="eventProducer">The event producer responsible for triggering events.</param>
        public SaleModifiedEventHandler(IEventProducer eventProducer)
            : base(eventProducer)
        {
            _eventProducer = eventProducer;
        }

        /// <summary>
        /// Handles the SaleModifiedEvent notification when a sale is modified.
        /// </summary>
        /// <param name="notification">The SaleModifiedEvent notification to handle.</param>
        /// <param name="cancellationToken">A cancellation token to observe while handling the event.</param>
        /// <returns>A task representing the asynchronous event handling operation.</returns>
        public override async Task Handle(SaleModifiedEvent notification, CancellationToken cancellationToken)
        {
            await _eventProducer.PublishAsync(notification, typeof(SaleModifiedEvent).Name);
        }
    }
}
