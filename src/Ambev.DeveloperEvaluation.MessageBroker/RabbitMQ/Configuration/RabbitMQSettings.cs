namespace Ambev.DeveloperEvaluation.MessageBroker.RabbitMQ.Configuration
{
    /// <summary>
    /// Represents the configuration settings required to connect to a RabbitMQ server.
    /// This class holds the necessary parameters such as the hostname, username, and password
    /// needed for establishing a connection to the RabbitMQ service.
    /// </summary>
    public class RabbitMQSettings
    {
        /// <summary>
        /// Gets or sets the hostname of the RabbitMQ server.
        /// This is the address of the server to which the application will connect.
        /// Default is "localhost".
        /// </summary>
        public string HostName { get; set; } = "localhost";

        /// <summary>
        /// Gets or sets the username used to authenticate with the RabbitMQ server.
        /// Default is "guest".
        /// </summary>
        public string UserName { get; set; } = "guest";

        /// <summary>
        /// Gets or sets the password used to authenticate with the RabbitMQ server.
        /// Default is "guest".
        /// </summary>
        public string Password { get; set; } = "guest";
    }
}
