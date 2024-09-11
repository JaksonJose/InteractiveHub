namespace Whatsapp.Core.Models.Receive
{
    public sealed class WhatsappText
    {
        /// <summary>
        /// This is the message that user writes and send
        /// </summary>
        public string Body { get; set; } = string.Empty;
    }
}