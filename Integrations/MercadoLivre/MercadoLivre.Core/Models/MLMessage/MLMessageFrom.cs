
using System.Text.Json.Serialization;

namespace MercadoLivre.Core.Models.MLMessage
{
    public class MLMessageFrom
    {
        /// <summary>
        /// The Id of the message sender.
        /// </summary>
        [JsonPropertyName("user_id")]
        public long UserId { get; set; }
    }
}
