using InteractiveLead.Core.Models;

namespace InteractiveLead.Core.NotifyServices
{
    public class MessageNotifyService
    {
        private readonly List<ClientSubscribe> _subscribedClients = [];

        public void Subscribe(ClientSubscribe client)
        {
            _subscribedClients.Add(client);
        }

        public void Unsubscribe(ClientSubscribe client)
        {
            _subscribedClients.RemoveAll(c => c.CompanyId == client.CompanyId && c.UserId == client.UserId);
        }

        public void NotifyNewLeadReceived(Lead lead)
        {
            var clienteToNotify = _subscribedClients.Where(c => c.UserId == lead.ConsultantId && c.CompanyId == lead.CompanyId).ToList();

            foreach (var client in clienteToNotify)
            {
                client.OnNewLeadReceived?.Invoke();
            }            
        }

        public void NotifyNewChatMessage(ChatMessage chatMessage)
        {
            var clienteToNotify = _subscribedClients.Where(c => c.LeadId == chatMessage.LeadId).ToList();

            foreach (var client in clienteToNotify)
            {
                client.OnNewChatMessage?.Invoke(chatMessage);
            }
        }
    }

    public sealed class ClientSubscribe
    {
        public long CompanyId { get; set; }

        public long UserId { get; set; }

        public long LeadId { get; set; }

        public bool CanReceiveMoreLeads { get; set; }

        public Action OnNewLeadReceived { get; set; } = default!;

        public Action<ChatMessage> OnNewChatMessage { get; set; } = default!;
    }
}
