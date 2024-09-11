
using System.Text.Json.Serialization;

namespace MercadoLivre.Core.Models.MLMessage
{
    public sealed class MLMessage
    {
        /// <summary>
        /// The id of the message
        /// </summary>
        public string Id { get; set; } = string.Empty;

        /// <summary>
        /// Message text sent
        /// </summary>
        public string Text { get; set; } = string.Empty;

        /// <summary>
        /// Who is sending the message
        /// </summary>
        public MLMessageFrom From { get; set; } = new();

        public MLMessageFrom To { get; set; } = new();

        /// <summary>
        /// This contains the important information to fetch the orders
        /// </summary>
        [JsonPropertyName("message_resources")]
        public List<MLMessageResource> MessageResources { get; set; } = [];

        /// <summary>
        /// Represents the date time of each action in the message
        /// </summary>
        [JsonPropertyName("message_date")]
        public MLMessageDate MessageDate { get; set; } = new();
    }
}
