
using CrossCutting.Models;
using InteractiveLead.Core.Interfaces.CentralHub;
using InteractiveLead.Core.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace InteractiveLead.Core.Broker
{
    public class MessageConsumer : BaseMessageConsumer
    {
        private readonly ILeadHubCentralBac _leadCentralHubBac;

        public MessageConsumer(ILeadHubCentralBac leadCentralHubBac) : base("leadmessage")
        {
            _leadCentralHubBac = leadCentralHubBac;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                string? mensagem = Encoding.UTF8.GetString(body);

                LeadMessage message = JsonSerializer.Deserialize<LeadMessage>(mensagem)!;

                await _leadCentralHubBac.ReceiveMessagerToChatAsync(message);

                _channel?.BasicAck(ea.DeliveryTag, false);
            };

            _channel.BasicConsume(queue: "leadmessage", autoAck: false, consumer: consumer);

            return Task.CompletedTask;
        }
    }
}
