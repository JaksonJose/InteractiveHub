namespace Whatsapp.Core.Models.Receive
{
    public sealed class WhatsappPayLoad
    {
        public List<WhatsappEntry> Entry { get; set; } = new();

        public string Object { get; set; } = string.Empty;        
    }
}
