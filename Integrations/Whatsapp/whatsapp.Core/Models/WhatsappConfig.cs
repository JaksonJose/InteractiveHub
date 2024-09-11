
namespace Whatsapp.Core.Models
{
    public sealed class WhatsappConfig
    {
        public int Id { get; set; }

        public int CompanyId { get; set; }

        public string AccessToken { get; set; } = string.Empty;

        public string BusinessAccountId { get; set; } = string.Empty;

        public string PhoneNumberId { get; set; } = string.Empty;
    }
}
