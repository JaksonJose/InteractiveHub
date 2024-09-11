namespace Whatsapp.Core.Models.Receive
{
    public sealed class WhatsappMessage
    {
        public string Id { get; set; } = string.Empty;

        public string From { get; set; } = string.Empty;

        public WhatsappText Text { get; set; } = new();

        public string TimeStamp { get; set; } = string.Empty;        

        public string Type { get; set; } = string.Empty;
    }
}