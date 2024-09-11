

using Whatsapp.Core.Models.Receive;

namespace whatsapp.Core.Interfaces.IBac
{
    public interface IWhatsappSendMessageBac
    {
        Task SendMessageToLeadsManagerAsync(WhatsappPayLoad whatsappMessage, long companyId);
    }
}
