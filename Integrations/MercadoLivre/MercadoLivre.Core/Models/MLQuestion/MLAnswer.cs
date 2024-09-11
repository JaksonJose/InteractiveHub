using System.Text.Json.Serialization;

namespace MercadoLivreTeste.App.Models.MLQuestions
{
    public class MLAnswer
    {
        public string Text { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;

        [JsonPropertyName("date_created")]
        public DateTimeOffset DateCreated { get; set; }
    }
}
