

namespace InteractiveLead.Core.Models
{
    public sealed class Consultant
    {
        public int Id { get; set; }

        /// <summary>
        /// Company where the user is registered
        /// </summary>
        public int CompanyId { get; set; }

        public string CompanyName { get; set; } = string.Empty;

        /// <summary>
        /// If the user is active
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// The user/consultant full name
        /// </summary>
        public string FullName { get; set; } = string.Empty;        

        /// <summary>
        /// Photo of the consultant/user
        /// </summary>
        public string PhotoUrl { get; set; } = string.Empty;

        /// <summary>
        /// Last time the lead was assigned to the consultant
        /// This applies in some lead distribuition rules
        /// </summary>
        public DateTime? TimeLastLeadAssigned { get; set; }

        /// <summary>
        /// Id to refer the AspNetUser
        /// </summary>
        public string AspNetUserId { get; set; } = string.Empty;
    }
}
