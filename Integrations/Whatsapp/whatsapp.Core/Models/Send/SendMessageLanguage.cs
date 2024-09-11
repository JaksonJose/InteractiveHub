using System.Text.Json.Serialization;

namespace Whatsapp.Core.Models.Send
{
    public sealed class SendMessageLanguage
    {
        /// <summary>
        /// Language code
        /// </summary>
        [JsonPropertyName("code")]
        public string Code { get; set; } = "pt_BR";
    }
}