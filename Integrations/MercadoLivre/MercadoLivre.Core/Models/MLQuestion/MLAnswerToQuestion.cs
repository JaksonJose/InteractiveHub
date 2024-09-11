
using System.Text.Json.Serialization;

namespace MercadoLivre.Core.Models.MLQuestion
{
    public sealed class MLAnswerToQuestion
    {
        [JsonPropertyName("question_id")]
        public long QuestionId { get; set; }

        [JsonPropertyName("text")]
        public string Text { get; set; } = string.Empty;
    }
}
