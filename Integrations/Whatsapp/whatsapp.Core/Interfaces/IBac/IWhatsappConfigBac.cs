
using Whatsapp.Core.Models;

namespace Whatsapp.Core.Interfaces.IBac
{
    public interface IWhatsappConfigBac
    {
        Task<WhatsappConfig> FetchWhatsappConfigByComapnyIdAsync(long companyId);

        Task<WhatsappConfig> FetchWhatsappConfigByPhoneNumberIdAsync(string phoneNumberId);
    }
}
