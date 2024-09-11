
using Whatsapp.Core.Models;

namespace Whatsapp.Core.Interfaces.IRepository
{
    public interface IWhatsappConfigRepository
    {
        Task<WhatsappConfig> FetchWhatsappConfigByComapnyIdAsync(long companyId);

        Task<WhatsappConfig> FetchWhatsappConfigByPhoneNumberIdAsync(string phoneNumberId);
    }
}
