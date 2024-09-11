
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;

namespace InteractiveLead.Core.Broker
{
    public class BaseMessageConsumer : BackgroundService
    {
        protected IConnection? _connection;
        protected IModel? _channel;
        private readonly string _queueName;

        protected BaseMessageConsumer(string queueName)
        {
            _queueName = queueName;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            var factory = new ConnectionFactory() { HostName = "rabbitmq" };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

            return base.StartAsync(cancellationToken);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.CompletedTask;
        }

        public override void Dispose()
        {
            _channel?.Close();
            _connection?.Close();
            base.Dispose();
        }
    }
}
