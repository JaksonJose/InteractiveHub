
namespace MercadoLivre.Core.Broker
{
    public interface IMessageBroker
    {
        void SendMessage(object message, string queueName);
    }
}
