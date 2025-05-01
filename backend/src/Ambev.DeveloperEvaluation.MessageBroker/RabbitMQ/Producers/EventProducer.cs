using Ambev.DeveloperEvaluation.MessageBroker.Abstractions;
using Ambev.DeveloperEvaluation.MessageBroker.RabbitMQ.Connections;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Ambev.DeveloperEvaluation.MessageBroker.RabbitMQ.Producers
{
    /// <summary>
    /// Class responsible for publishing events to a RabbitMQ queue.
    /// </summary>
    public class EventProducer : IEventProducer
    {
        /// <summary>
        /// The connection manager responsible for managing the connection to RabbitMQ.
        /// </summary>
        private readonly RabbitMQConnectionManager _connectionManager;

        /// <summary>
        /// Initializes a new instance of the EventProducer class.
        /// </summary>
        /// <param name="connectionManager">The RabbitMQ connection manager to manage the connection to RabbitMQ server.</param>
        public EventProducer(RabbitMQConnectionManager connectionManager)
        {
            _connectionManager = connectionManager;
        }

        /// <inheritdoc/>
        public async Task PublishAsync<T>(T message, string queueName) where T : class
        {
            var connection = await _connectionManager.GetConnectionAsync();

            using var channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));

            var basicProperties = new BasicProperties
            {
                Persistent = true
            };

            await channel.BasicPublishAsync(
                exchange: "",
                routingKey: queueName,
                mandatory: true,
                basicProperties: basicProperties,
                body: body);

            Console.WriteLine($"Event published: {queueName}");
        }
    }
}
