namespace Ambev.DeveloperEvaluation.MessageBroker.Abstractions
{
    /// <summary>
    /// Interface responsible for publishing events to a RabbitMQ queue.
    /// </summary>
    public interface IEventProducer
    {

        /// <summary>
        /// Publishes an event message to a specified RabbitMQ queue.
        /// </summary>
        /// <typeparam name="T">The type of the event message.</typeparam>
        /// <param name="message">The message to be published to the queue.</param>
        /// <param name="queueName">The name of the RabbitMQ queue where the message will be published.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task PublishAsync<T>(T message, string queueName) where T : class;
    }
}
