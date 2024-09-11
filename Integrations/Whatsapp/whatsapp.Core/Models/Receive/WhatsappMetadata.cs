using System.Text.Json.Serialization;

namespace Whatsapp.Core.Models.Receive
{
    public sealed class WhatsappMetadata
    {
        [JsonPropertyName("Display_Phone_Number")]
        public string DisplayPhoneNumber { get; set; } = string.Empty;

        [JsonPropertyName("Phone_Number_Id")]
        public string PhoneNumberId { get; set; } = string.Empty;
    }
}