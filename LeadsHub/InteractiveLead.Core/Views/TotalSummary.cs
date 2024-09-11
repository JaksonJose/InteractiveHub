
namespace InteractiveLead.Core.Views
{
    public sealed class TotalSummary
    {
        /// <summary>
        /// Total of all leads, but finished, cancled or refused ones
        /// </summary>
        public int TotalAllLeads { get; set; }

        /// <summary>
        /// Total of new leads
        /// </summary>
        public int TotalNewLeads { get; set; }

        /// <summary>
        /// Total of leads in progress
        /// </summary>
        public int TotalInProgress { get; set; }

        /// <summary>
        /// Total of leads scheduled
        /// </summary>
        public int TotalScheduled { get; set; }

        /// <summary>
        /// Total of lead made a question
        /// </summary>
        public int TotalNewQuestions { get; set; }

        /// <summary>
        /// Total of question answered by the consultant
        /// </summary>
        public int TotalQuestionAnswered { get; set; }
    }
}
