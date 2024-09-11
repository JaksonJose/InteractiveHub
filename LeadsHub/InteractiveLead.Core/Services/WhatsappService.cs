using AdaptiveKitCore.Responses;
using CrossCutting.Enums;
using CrossCutting.Models;
using InteractiveLead.Core.Interfaces.IServices;
using InteractiveLead.Core.Models;
using System.Text.Json;

namespace InteractiveLead.Core.Services
{
    public sealed class WhatsappService : IWhatsappService
    {
        private readonly IBaseService _baseService;

        public WhatsappService(IBaseService service)
        {
            _baseService = service;
        }

        public async Task<BaseResponse> SendMessageToIntegrationApiAsync(Lead lead, ChatMessage chatMessage)
        {
            BaseResponse response = new();

            // Object to send
            LeadMessage message = new()
            {
                CompanyId = lead.CompanyId,
                MessageBody = chatMessage.MessageBody,
                PhoneNumber = lead.PhoneNumber,
                MessageType = chatMessage.MessageType
            };

            string messageSerialized = JsonSerializer.Serialize(message);

            if (ChannelTypeEnum.Whatsapp.Equals(ChannelTypeEnum.FromName(lead.SourceChannel)))
            {
                response = await _baseService.SendMessageAsync(messageSerialized, "http://whatsapp_api:8080/api/whatsapp/sendmessage");
            }

            if (ChannelTypeEnum.MercadoLivre.Equals(ChannelTypeEnum.FromName(lead.SourceChannel)))
            {
                response = await _baseService.SendMessageAsync(messageSerialized, "http://localhost:7500/api/mercadolivre/message");
            }

            return response;
        }
    }
}
