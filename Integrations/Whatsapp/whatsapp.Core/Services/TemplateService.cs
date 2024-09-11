
using Whatsapp.Core.Models;
using Whatsapp.Core.Request;
using Whatsapp.Core.Utility;
using Whatsapp.Core.Interfaces.IBac;
using Whatsapp.Core.Interfaces.IServices;
using whatsapp.Core.Interfaces.IServices;
using whatsapp.Core.Response;

namespace whatsapp.Core.Services
{
    public sealed class TemplateService : ITemplateService
    {
        private readonly IBaseService _baseService;
        private readonly IWhatsappConfigBac _whatsappConfigBac;

        public TemplateService(IBaseService baseService,
            IWhatsappConfigBac whatsappConfigBac)
        {
            _baseService = baseService;
            _whatsappConfigBac = whatsappConfigBac;
        }

        public async Task<JsonResponse> FetchTemplatesAsync(long empresaId)
        {
            MessageRequest request = new();

            WhatsappConfig whatsappConfig = await _whatsappConfigBac.FetchWhatsappConfigByComapnyIdAsync(empresaId);

            request.AccessToken = whatsappConfig.AccessToken;
            request.Url = SD.WhatsappAPIBase + $"/{whatsappConfig.BusinessAccountId}/message_templates";

            JsonResponse response = await _baseService.GetAsync(request);

            return response;
        }
    }
}
