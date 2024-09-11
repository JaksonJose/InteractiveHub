namespace Whatsapp.Core.Models.Receive
{
    public sealed class WhatsappValue
    {
        public string Messaging_Product { get; set; } = string.Empty;

        public WhatsappMetadata Metadata { get; set; } = new();

        public List<WhatsappContact> Contacts { get; set; } = new();

        public List<WhatsappMessage> Messages { get; set; } = new();
    }
}