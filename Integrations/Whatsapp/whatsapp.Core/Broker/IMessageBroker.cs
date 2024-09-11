
namespace whatsapp.Core.Broker
{
    public interface IMessageBroker
    {
        void SendMessage(object message, string queueName);
    }
}
