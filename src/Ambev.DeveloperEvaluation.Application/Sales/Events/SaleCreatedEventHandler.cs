using Ambev.DeveloperEvaluation.Domain.Events.Sales;
using Ambev.DeveloperEvaluation.MessageBroker.Abstractions;

namespace Ambev.DeveloperEvaluation.Application.Sales.Events
{
    /// <summary>
    /// Event handler for handling the creation of a sale. 
    /// Inherits from a generic EventHandler to process SaleCreatedEvent notifications.
    /// </summary>
    public class SaleCreatedEventHandler : EventHandler<SaleCreatedEvent>
    {
        private readonly IEventProducer _eventProducer;

        /// <summary>
        /// Initializes a new instance of the SaleCreatedEventHandler class.
        /// </summary>
        /// <param name="eventProducer">The event producer responsible for triggering events.</param>
        public SaleCreatedEventHandler(IEventProducer eventProducer)
            : base(eventProducer)
        {
            _eventProducer = eventProducer;
        }

        /// <summary>
        /// Handles the SaleCreatedEvent notification when a sale is created.
        /// </summary>
        /// <param name="notification">The SaleCreatedEvent notification to handle.</param>
        /// <param name="cancellationToken">A cancellation token to observe while handling the event.</param>
        /// <returns>A task representing the asynchronous event handling operation.</returns>
        public override async Task Handle(SaleCreatedEvent notification, CancellationToken cancellationToken)
        {
            await _eventProducer.PublishAsync(notification, typeof(SaleCreatedEvent).Name);
        }
    }
}
