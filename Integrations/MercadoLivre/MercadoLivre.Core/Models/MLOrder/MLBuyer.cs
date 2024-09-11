
using System.Text.Json.Serialization;

namespace MercadoLivre.Core.Models.MLOrder
{
    public sealed class MLBuyer
    {
        public long Id { get; set; }

        public string Nickname { get; set; } = string.Empty;

        [JsonPropertyName("first_name")]
        public string FirstName { get; set; } = string.Empty;

        [JsonPropertyName("last_name")]
        public string LastName { get; set; } = string.Empty;
    }
}
