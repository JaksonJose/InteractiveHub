
using System.Text.Json;
using System.Text;
using RabbitMQ.Client;

namespace whatsapp.Core.Broker
{
    public class MessageBroker : IMessageBroker
    {
        private readonly string _hostName;
        private readonly string _userName;
        private readonly string _password;

        private IConnection? _connection;

        public MessageBroker()
        {
            _hostName = "rabbitmq";
            _password = "guest";
            _userName = "guest";
        }

        public void SendMessage(object message, string queueName)
        {
            if (ConnectiionExists())
            {
                using var channel = _connection?.CreateModel();
                channel?.QueueDeclare(queueName, false, false, false, null);

                string? json = JsonSerializer.Serialize(message);
                byte[] body = Encoding.UTF8.GetBytes(json);

                channel?.BasicPublish(exchange: "", routingKey: queueName, null, body: body);
            }
        }

        private void CreateConnection()
        {
            try
            {
                ConnectionFactory connectionFactory = new()
                {
                    HostName = _hostName,
                    UserName = _userName,
                    Password = _password
                };

                _connection = connectionFactory.CreateConnection();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private bool ConnectiionExists()
        {
            if (_connection != null)
            {
                return true;
            }

            CreateConnection();

            return true;
        }
    }
}
