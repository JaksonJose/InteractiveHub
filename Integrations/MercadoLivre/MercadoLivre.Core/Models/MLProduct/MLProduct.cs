

using System.Text.Json.Serialization;

namespace MercadoLivre.Core.Models.MLProduct
{
    public class MLProduct
    {
        /// <summary>
        /// Link to redirect to product page
        /// </summary>
        
        public string Permalink { get; set; } = string.Empty;

        /// <summary>
        /// Pricture of products
        /// </summary>
        [JsonPropertyName("pictures")]
        public List<MLPictures> Prictures { get; set; } = [];
    }
}
