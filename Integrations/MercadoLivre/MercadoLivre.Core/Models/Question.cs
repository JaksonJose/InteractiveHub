
namespace MercadoLivre.Core.Models
{
    /// <summary>
    /// The question received
    /// </summary>
    public sealed class Question
    {
        public long Id { get; set; }

        /// <summary>
        /// This is the buyer Id from portal
        /// this identify the buyer in the portal
        /// </summary>
        public long BuyerId { get; set; }        

        /// <summary>
        /// The channel where the question come from
        /// Ex.: Mercado Livre
        /// </summary>
        public string ChannelType { get; set; } = string.Empty;

        /// <summary>
        /// Identification of the company that
        /// the lead belongs
        /// </summary>
        public long CompanyId { get; set; }

        /// <summary>
        /// The consutant assigned to 
        /// answer the question
        /// </summary>
        public long ConsultantId { get; set; }

        /// <summary>
        /// This is the date time from the question / answer
        /// </summary>
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// This is the product Id from portal
        /// </summary>
        public string ItemId { get; set; } = string.Empty;

        /// <summary>
        /// Identifies the lead that is 
        /// making the question.
        /// </summary>
        public string LeadIdentifier { get; set; } = string.Empty;

        /// <summary>
        /// This is the question or the answer message text
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// This is the question Id from portal
        /// </summary>
        public long PortalQuestionId { get; set; }

        /// <summary>
        /// Product Image mercado Livre URL
        /// </summary>
        public string ProductImageUrl { get; set; } = string.Empty;


        /// <summary>
        /// Link to the product page
        /// </summary>
        public string ProductLink { get; set; } = string.Empty;

        /// <summary>
        /// The answer of the question
        /// </summary>
        public Answer? Answer { get; set; }

    }
}
