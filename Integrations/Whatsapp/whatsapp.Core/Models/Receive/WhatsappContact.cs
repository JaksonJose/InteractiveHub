using System.Text.Json.Serialization;

namespace Whatsapp.Core.Models.Receive
{
    public sealed class WhatsappContact
    {
        public WhatsappProfile Profile { get; set; } = new();

        [JsonPropertyName("wa_id")]
        public string WaId { get; set; } = string.Empty;
    }
}