using System.Text.Json.Serialization;

namespace MercadoLivreTeste.App.Models.MLQuestions
{
    public class MLQuestion
    {
        public long Id { get; set; }

        [JsonPropertyName("seller_id")]
        public long SellerId { get; set; }

        public string Text { get; set; } = string.Empty;

        public string Tags { get; set; } = string.Empty;

        [JsonPropertyName("item_id")]
        public string ItemId { get; set; } = string.Empty;

        [JsonPropertyName("date_created")]
        public DateTime DateCreated { get; set; }

        public bool Hold { get; set; }

        [JsonPropertyName("deleted_from_listing")]
        public bool DeletedFromListing { get; set; }

        public MLAnswer Answer { get; set; } = new();

        public MLQuestionFrom From { get; set; } = new();
    }
}
