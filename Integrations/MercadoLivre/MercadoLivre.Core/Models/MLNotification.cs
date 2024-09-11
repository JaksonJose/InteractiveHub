
using System.Text.Json.Serialization;

namespace MercadoLivre.Core.Models
{
    public class MLNotification
    {
        [JsonPropertyName("_id")]
        public string Id { get; set; } = string.Empty;

        [JsonPropertyName("resource")]
        public string Resource { get; set; } = string.Empty;

        [JsonPropertyName("user_id")]
        public long UserId { get; set; }

        [JsonPropertyName("topic")]
        public string Topic { get; set; } = string.Empty;

        [JsonPropertyName("application_id")]
        public long ApplicationId { get; set; }

        [JsonPropertyName("attempts")]
        public int Attempts { get; set; }

        [JsonPropertyName("sent")]
        public string Sent { get; set; } = string.Empty;

        [JsonPropertyName("received")]
        public string Received { get; set; } = string.Empty;
    }
}
