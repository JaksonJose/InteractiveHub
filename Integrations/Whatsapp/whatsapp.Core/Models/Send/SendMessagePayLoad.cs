﻿
using System.Text.Json.Serialization;

namespace Whatsapp.Core.Models.Send
{
    public sealed class SendMessagePayLoad
    {
        /// <summary>
        /// Meta Product to be applied
        /// </summary>
        [JsonPropertyName("messaging_product")]
        public string MessagingProduct { get; private set; } = "whatsapp";

        [JsonPropertyName("recipient_type")]
        public string RecepientType { get; set; } = "individual";

        /// <summary>
        /// Template reference to send
        /// </summary>
        [JsonPropertyName("template")]
        public SendMessageTemplate Template { get; set; } = new();

        /// <summary>
        /// Text message to send to customer.
        /// </summary>
        [JsonPropertyName("text")]
        public TextMessage Text { get; set; } = new();

        /// <summary>
        /// Receiver numper
        /// </summary>
        [JsonPropertyName("to")]
        public string To { get; set; } = string.Empty;

        /// <summary>
        /// Type of message (Ex.: Template)
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; } = "template";
    }
}
