
using Whatsapp.Core.Interfaces.IBac;
using Whatsapp.Core.Interfaces.IRepository;
using Whatsapp.Core.Models;

namespace Whatsapp.Core.Bac
{
    public class WhatsappConfigBac : IWhatsappConfigBac
    {
        private readonly IWhatsappConfigRepository _WhatsappConfigrepository;
        public WhatsappConfigBac(IWhatsappConfigRepository whatsappConfigRepository)
        {
            _WhatsappConfigrepository = whatsappConfigRepository;
        }

        public async Task<WhatsappConfig> FetchWhatsappConfigByComapnyIdAsync(long companyId)
        {
            WhatsappConfig response = await _WhatsappConfigrepository.FetchWhatsappConfigByComapnyIdAsync(companyId);

            return response;
        }

        public async Task<WhatsappConfig> FetchWhatsappConfigByPhoneNumberIdAsync(string phoneNumberId)
        {
            WhatsappConfig response = await _WhatsappConfigrepository.FetchWhatsappConfigByPhoneNumberIdAsync(phoneNumberId);

            return response;
        }
    }
}
