using RabbitMQ.Client;
using System.Text;

namespace ProducerForRabbitMQ
{
    public class CoreMessage : ICoreMessage
    {
        private readonly ConnectionFactory _rabbitMQServer;
        private readonly string _queueName;
        public CoreMessage(string hostName, string queueName)
        {
            _rabbitMQServer = new ConnectionFactory() { HostName = hostName, Password="guest", UserName="guest" };
            _queueName = queueName;
        }

        public void SendMessage(string message)
        {
            // create connection
            using var connection = _rabbitMQServer.CreateConnection();

            // create channel
            using var channel = connection.CreateModel();

            // connect to the queue
            channel.QueueDeclare(queue: _queueName,
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            // we need to write data in the form of bytes
            var bodyMessage = Encoding.UTF8.GetBytes(message);

            // push content into the queue
            channel.BasicPublish(exchange: "",
                                 routingKey: _queueName,
                                 basicProperties: null,
                                 body: bodyMessage);
        }
    }
}