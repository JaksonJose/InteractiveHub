
using System.ComponentModel.DataAnnotations.Schema;

namespace InteractiveLead.Core.Models
{
    public sealed class Lead
    {
        #region Mapped Properties
        public long Id { get; set; }

        public long CompanyId { get; set; }

        /// <summary>
        /// Consultant identifier
        /// </summary>
        public long ConsultantId { get; set; }

        /// <summary>
        /// Email of the lead
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// A helper to identify the unique client in the platform
        /// </summary>
        public string Identifier { get; set; } = string.Empty;

        /// <summary>
        /// Name of the lead
        /// </summary>
        public string Name { get; set; } = string.Empty;

        public string PathReferece { get; set; } = string.Empty;

        /// <summary>
        /// Phone of the lead
        /// </summary>
        public string PhoneNumber { get; set; } = string.Empty;

        public string SourceChannel { get; set; } = string.Empty;

        /// <summary>
        /// Status of the lead
        /// Ex.: New, InProgress, Scheduled, Closed
        /// </summary>
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// Date that lead was created
        /// </summary>
        public DateTime CreatedAt { get; set; }
        #endregion

        /// <summary>
        /// The lead has one Consultant.
        /// This is used when fetch Lead and the consultant data is important.
        /// </summary>
        [NotMapped]
        public Consultant Consultant { get; set; } = new();

        /// <summary>
        /// Chat mensages of a lead.
        /// </summary>
        [NotMapped]
        public List<ChatMessage> ChatMessages { get; set; } = [];

        [NotMapped]
        public long TotalNewMessage { get; set; }
    }
}
