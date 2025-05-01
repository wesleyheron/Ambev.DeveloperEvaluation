using Ambev.DeveloperEvaluation.Domain.Events.Sales;
using Ambev.DeveloperEvaluation.MessageBroker.Abstractions;

namespace Ambev.DeveloperEvaluation.Application.Sales.Events
{
    /// <summary>
    /// Event handler for handling the cancellation of a sale. 
    /// Inherits from a generic EventHandler to process SaleCancelledEvent notifications.
    /// </summary>
    public class SaleCancelledEventHandler : EventHandler<SaleCancelledEvent>
    {
        private readonly IEventProducer _eventProducer;
        /// <summary>
        /// Initializes a new instance of the SaleCancelledEventHandler class.
        /// </summary>
        /// <param name="eventProducer">The event producer responsible for triggering events.</param>
        public SaleCancelledEventHandler(IEventProducer eventProducer)
            : base(eventProducer)
        {
            _eventProducer = eventProducer;
        }

        /// <summary>
        /// Handles the SaleCancelledEvent notification when a sale is cancelled.
        /// </summary>
        /// <param name="notification">The SaleCancelledEvent notification to handle.</param>
        /// <param name="cancellationToken">A cancellation token to observe while handling the event.</param>
        /// <returns>A task representing the asynchronous event handling operation.</returns>
        public override async Task Handle(SaleCancelledEvent notification, CancellationToken cancellationToken)
        {
            await _eventProducer.PublishAsync(notification, typeof(SaleCancelledEvent).Name);
        }
    }

}
