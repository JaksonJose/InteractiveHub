
using AdaptiveKitCore.Responses;
using CrossCutting.Enums;
using InteractiveLead.Core.Models;

namespace InteractiveLead.Core.Interfaces.IServices
{
    public interface IWhatsappService
    {
        Task<BaseResponse> SendMessageToIntegrationApiAsync(Lead lead, ChatMessage chatMessage);
    }
}
