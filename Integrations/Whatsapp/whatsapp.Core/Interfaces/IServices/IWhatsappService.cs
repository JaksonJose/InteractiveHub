
using CrossCutting.Models;
using whatsapp.Core.Response;

namespace Whatsapp.Core.Interfaces.IServices
{
    public interface IWhatsappService
    {
        /// <summary>
        /// Send message to whatsapp number
        /// </summary>
        /// <returns></returns>
        Task<JsonResponse> SendMessageToWhatsappAsync(LeadMessage request);
    }
}
