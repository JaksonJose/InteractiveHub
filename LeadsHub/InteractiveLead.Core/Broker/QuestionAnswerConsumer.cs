
using InteractiveLead.Core.Interfaces.CentralHub;
using InteractiveLead.Core.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace InteractiveLead.Core.Broker
{
    public class QuestionAnswerConsumer : BaseMessageConsumer
    {
        private readonly IQuestionCentralHubBac _questionCentralBac;

        public QuestionAnswerConsumer(IQuestionCentralHubBac questionCentralBac) : base("question-answer")
        {
            _questionCentralBac = questionCentralBac;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                string? mensagem = Encoding.UTF8.GetString(body);

                Question question = JsonSerializer.Deserialize<Question>(mensagem)!;

                await _questionCentralBac.ReceiveQuestionAnswerAsync(question);

                // TODO: Implement a validation to rid of the message or keep it.

                _channel?.BasicAck(ea.DeliveryTag, false);
            };

            _channel.BasicConsume(queue: "question-answer", autoAck: false, consumer: consumer);

            return Task.CompletedTask;
        }
    }
}
