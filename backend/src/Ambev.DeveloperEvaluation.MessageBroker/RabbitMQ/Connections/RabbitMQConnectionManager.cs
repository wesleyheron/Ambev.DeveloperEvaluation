using RabbitMQ.Client;

namespace Ambev.DeveloperEvaluation.MessageBroker.RabbitMQ.Connections
{
    /// <summary>
    /// Manages the connection to a RabbitMQ server by creating and providing an active connection.
    /// This class uses the RabbitMQ ConnectionFactory to configure and establish a connection
    /// to the RabbitMQ server using specified credentials and server details.
    /// </summary>
    public class RabbitMQConnectionManager
    {
        private readonly ConnectionFactory _connectionFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="RabbitMQConnectionManager"/> class.
        /// This constructor sets up the connection factory with the provided RabbitMQ server details
        /// such as hostname, username, and password.
        /// </summary>
        /// <param name="hostName">The hostname or IP address of the RabbitMQ server.</param>
        /// <param name="userName">The username used to authenticate with the RabbitMQ server.</param>
        /// <param name="password">The password associated with the username for RabbitMQ authentication.</param>
        public RabbitMQConnectionManager(string hostName, string userName, string password)
        {
            _connectionFactory = new ConnectionFactory
            {
                HostName = hostName,
                UserName = userName,
                Password = password
            };
        }

        /// <summary>
        /// Asynchronously creates and returns a connection to the RabbitMQ server.
        /// </summary>
        /// <returns>A task representing the asynchronous operation. The task result is the established connection to RabbitMQ.</returns>
        public async Task<IConnection> GetConnectionAsync()
        {
            return await _connectionFactory.CreateConnectionAsync();
        }
    }
}
