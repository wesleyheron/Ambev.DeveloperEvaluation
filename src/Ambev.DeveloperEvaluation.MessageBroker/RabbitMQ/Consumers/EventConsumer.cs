using Ambev.DeveloperEvaluation.MessageBroker.Common;
using Ambev.DeveloperEvaluation.MessageBroker.RabbitMQ.Connections;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace Ambev.DeveloperEvaluation.MessageBroker.RabbitMQ.Consumers
{
    /// <summary>
    /// A consumer class that asynchronously consumes events from a RabbitMQ queue.
    /// It listens to a specified queue and processes incoming events by delegating to an event handler.
    /// </summary>
    /// <typeparam name="TEvent">The type of event to be consumed. This type must be a class.</typeparam>
    public class EventConsumer<TEvent> where TEvent : class
    {
        /// <summary>
        /// The connection manager responsible for managing the connection to RabbitMQ.
        /// </summary>
        private readonly RabbitMQConnectionManager _connectionManager;

        /// <summary>
        /// The event consumer responsible for handling and processing the consumed events.
        /// </summary>
        private readonly IEventConsumer<TEvent> _eventConsumer;


        /// <summary>
        /// Initializes a new instance of the <see cref="EventConsumer{TEvent}"/> class.
        /// </summary>
        /// <param name="connectionManager">The connection manager responsible for managing RabbitMQ connections.</param>
        /// <param name="eventConsumer">The event consumer responsible for handling the events.</param>
        public EventConsumer(RabbitMQConnectionManager connectionManager, IEventConsumer<TEvent> eventConsumer)
        {
            _connectionManager = connectionManager;
            _eventConsumer = eventConsumer;
        }

        /// <summary>
        /// Asynchronously consumes messages from the specified RabbitMQ queue.
        /// This method connects to RabbitMQ, listens for incoming messages, and delegates the event processing
        /// to the provided <see cref="IEventConsumer{TEvent}"/> instance.
        /// </summary>
        /// <param name="queueName">The name of the RabbitMQ queue to listen to.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task ConsumeAsync(string queueName)
        {
            var connection = await _connectionManager.GetConnectionAsync();
            var channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(
                queue: queueName,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            var consumer = new AsyncEventingBasicConsumer(channel);

            consumer.ReceivedAsync += async (model, ea) =>
            {
                try
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);

                    var @event = JsonSerializer.Deserialize<TEvent>(message);

                    if (@event != null)
                    {
                        await _eventConsumer.ConsumeAsync(@event);
                    }

                    await Task.CompletedTask;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing message: {ex.Message}");
                }
            };

            await channel.BasicConsumeAsync(
                queue: queueName,
                autoAck: true,
                consumer: consumer);

            await Task.Delay(-1);
        }
    }
}
