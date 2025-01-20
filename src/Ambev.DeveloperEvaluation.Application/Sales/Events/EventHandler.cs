using Ambev.DeveloperEvaluation.Application.Abstractions.Messaging;
using Ambev.DeveloperEvaluation.MessageBroker.Abstractions;

namespace Ambev.DeveloperEvaluation.Application.Sales.Events
{
    /// <summary>
    /// A base class for handling domain events. It publishes events to an event producer and handles exceptions.
    /// </summary>
    /// <typeparam name="TEvent">The type of event that this handler will process.</typeparam>
    public class EventHandler<TEvent> : IDomainEventHandler<TEvent> where TEvent : class
    {
        private readonly IEventProducer _eventProducer;

        /// <summary>
        /// Initializes a new instance of the EventHandler class.
        /// </summary>
        /// <param name="eventProducer">The event producer used to publish events.</param>
        public EventHandler(IEventProducer eventProducer)
        {
            _eventProducer = eventProducer;
        }

        /// <summary>
        /// Handles the domain event and publishes it using the event producer.
        /// </summary>
        /// <param name="notification">The event notification to handle.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the event handling.</param>
        /// <returns>A task representing the asynchronous operation of handling the event.</returns>
        public virtual async Task Handle(TEvent notification, CancellationToken cancellationToken)
        {
            try
            {
                var queueName = typeof(TEvent).Name;

                await _eventProducer.PublishAsync(notification, queueName);
            }
            catch (Exception ex)
            {
                var errorMessage = new
                {
                    Success = false,
                    Message = $"{ex.Message}-{ex.InnerException?.Message}"
                };

                await _eventProducer.PublishAsync(errorMessage, "errorQueue");
                throw;
            }
        }
    }
}
