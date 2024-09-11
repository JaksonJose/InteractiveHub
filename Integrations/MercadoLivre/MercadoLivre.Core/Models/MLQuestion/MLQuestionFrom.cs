using System.Text.Json.Serialization;

namespace MercadoLivreTeste.App.Models.MLQuestions
{
    /// <summary>
    /// It represents the user that sent the question
    /// </summary>
    public class MLQuestionFrom
    {
        /// <summary>
        /// User account Id (future buyer)
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// How many question was answered
        /// </summary>
        [JsonPropertyName("answered_questions")]
        public int AnsweredQuestions { get; set; }
    }
}
