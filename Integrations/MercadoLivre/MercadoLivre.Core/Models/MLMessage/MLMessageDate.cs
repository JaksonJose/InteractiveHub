
namespace MercadoLivre.Core.Models.MLMessage
{
    /// <summary>
    /// This class represent the time of action of a message.
    /// </summary>
    public sealed class MLMessageDate
    {
        public DateTime Received { get; set; }

        public DateTime Available { get; set; }

        public DateTime Notified { get; set; }

        public DateTime Created { get; set; }

        public DateTime Read { get; set; }
    }
}
