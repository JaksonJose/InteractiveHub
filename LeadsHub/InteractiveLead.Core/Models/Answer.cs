

namespace InteractiveLead.Core.Models
{
    public sealed class Answer
    {
        public int Id { get; set; }

        /// <summary>
        /// This is the questionId from question persisted
        /// in the question database
        /// </summary>
        public long QuestionId { get; set; }

        /// <summary>
        /// This is the question Id from portal
        /// </summary>
        public long PortalQuestionId { get; set; }

        /// <summary>
        /// This is the question or the answer message text
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// Date of the answer
        /// </summary>
        public DateTime DateCreated { get; set; }
    }
}
