namespace Whatsapp.Core.Models.Receive
{
    public sealed class WhatsappChange
    {
        public string Field { get; set; } = string.Empty;

        public WhatsappValue Value { get; set; } = new();        
    }
}
