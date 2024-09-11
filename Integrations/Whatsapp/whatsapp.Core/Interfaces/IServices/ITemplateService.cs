using whatsapp.Core.Response;

namespace whatsapp.Core.Interfaces.IServices
{
    public interface ITemplateService
    {
        Task<JsonResponse> FetchTemplatesAsync(long empresaId);
    }
}
