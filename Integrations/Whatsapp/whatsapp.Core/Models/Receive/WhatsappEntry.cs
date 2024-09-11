namespace Whatsapp.Core.Models.Receive
{
    public sealed class WhatsappEntry
    {
        public List<WhatsappChange> Changes { get; set; } = new();

        public string Id { get; set; } = string.Empty;
        
    }
}