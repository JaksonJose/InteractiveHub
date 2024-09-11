
using System.Text.Json.Serialization;

namespace MercadoLivre.Core.Models.MLMessagesPack
{
    public sealed class MLMessagesPack
    {
        [JsonPropertyName("conversation_status")]
        public MLConversationStatus ConversationStatus { get; set; } = new();
    }
}
