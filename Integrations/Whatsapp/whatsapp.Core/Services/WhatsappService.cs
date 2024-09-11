using Whatsapp.Core.Interfaces.IBac;
using Whatsapp.Core.Interfaces.IServices;
using Whatsapp.Core.Models;
using Whatsapp.Core.Models.Send;
using Whatsapp.Core.Request;
using Whatsapp.Core.Utility;
using System.Text.Json;
using CrossCutting.Models;
using whatsapp.Core.Response;

namespace Whatsapp.Core.Services
{
    public sealed class WhatsappService : IWhatsappService
    {
        private readonly IBaseService _baseService;
        private readonly IWhatsappConfigBac _whatsappConfigBac;

        public WhatsappService(IBaseService baseService, 
            IWhatsappConfigBac whatsappConfigBac)
        {
            _baseService = baseService;
            _whatsappConfigBac = whatsappConfigBac;
        }

        public async Task<JsonResponse> SendMessageToWhatsappAsync(LeadMessage chatRequest)
        {
            MessageRequest request = new();

            WhatsappConfig whatsappConfig = await _whatsappConfigBac.FetchWhatsappConfigByComapnyIdAsync(chatRequest.CompanyId);

            // TODO: Message, template
            // TODO: Create the method to send audio, video and image.
            SendMessagePayLoad sendMessagePayLoad = new()
            {
                RecepientType = "individual",
                To = chatRequest.PhoneNumber,
                Type = chatRequest.MessageType, // Determinates if it is template or text
                Template = new()
                {
                    Name = "template_teste", //should use the template name
                    Language = new()
                    {
                        Code = "pt_BR",
                    }
                },
                Text = new()
                {
                    PreviewUrl = false,
                    Body = chatRequest.MessageBody
                }
            };

            request.AccessToken = whatsappConfig.AccessToken;

            request.Url = SD.WhatsappAPIBase + $"/{whatsappConfig.PhoneNumberId}/messages";
            request.DataJson = JsonSerializer.Serialize(sendMessagePayLoad);

            JsonResponse response = await _baseService.SendMessageAsync(request);

            return response;
        }
    }
}
