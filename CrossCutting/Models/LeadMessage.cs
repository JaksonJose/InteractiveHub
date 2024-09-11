
namespace CrossCutting.Models
{
    public class LeadMessage
    {
        public long CompanyId { get; set; }

        /// <summary>
        /// A helper to identify the unique client in the platform
        /// </summary>
        public string Identifier { get; set; } = string.Empty;

        /// <summary>
        /// Message to send or receive, if it is a text;
        /// </summary>
        public string MessageBody { get; set; } = string.Empty;

        /// <summary>
        /// Date and time that the message was sent to LeadsManager
        /// </summary>
        public DateTime MessageDate { get; set; }

        /// <summary>
        /// Type of message (template or text)
        /// </summary>
        public string MessageType { get; set; } = "template";

        /// <summary>
        /// Customer / Seller Name
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// This can represents a part of a uri or routes,
        /// Most used in the Mercado Livre api
        /// </summary>
        public string PathReference { get; set; } = string.Empty;

        /// <summary>
        /// Customer Social Media Phone Number
        /// </summary>
        public string PhoneNumber { get; set; } = string.Empty;

        /// <summary>
        /// Which channel the message comes from.
        /// </summary>
        public string SourceChannel { get; set; } = string.Empty;
    }
}
