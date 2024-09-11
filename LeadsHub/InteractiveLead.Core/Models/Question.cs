
using System.ComponentModel.DataAnnotations.Schema;

namespace InteractiveLead.Core.Models
{
    public sealed class Question
    {
        public long Id { get; set; }

        /// <summary>
        /// This is the question Id from portal
        /// </summary>
        public long PortalQuestionId { get; set; }

        public long CompanyId { get; set; }

        public long ConsultantId { get; set; }

        public string ChannelType { get; set; } = string.Empty;

        /// <summary>
        /// This is the question or the answer message text
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// This is the product Id from portal
        /// </summary>
        public string ItemId { get; set; } = string.Empty;

        /// <summary>
        /// This is the buyer Id from portal
        /// this identify the buyer in the portal
        /// </summary>
        public long BuyerId { get; set; }

        /// <summary>
        /// This is the date time from the question / answer
        /// </summary>
        public DateTime DateCreated { get; set; }

        public Answer? Answer { get; set; }

        [NotMapped]
        public Consultant? Consultant { get; set; }
    }
}
