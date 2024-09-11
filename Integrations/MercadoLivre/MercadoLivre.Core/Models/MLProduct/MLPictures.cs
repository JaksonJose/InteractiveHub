using System.Text.Json.Serialization;

namespace MercadoLivre.Core.Models.MLProduct
{
    public class MLPictures
    {
        /// <summary>
        /// Image Id
        /// </summary>
        public string Id { get; set; } = string.Empty;

        /// <summary>
        /// Url of image stored, http
        /// </summary>
        public string Url { get; set; } = string.Empty;

        /// <summary>
        /// Url of image stored, https
        /// </summary>
        [JsonPropertyName("secure_url")]
        public string SecureUrl { get; set; } = string.Empty;

        /// <summary>
        /// Size of image
        /// </summary>
        public string Size { get; set; } = string.Empty;

        /// <summary>
        /// Maximum size of the image
        /// </summary>
        [JsonPropertyName("max_size")]
        public string MaxSize { get; set; } = string.Empty;

        public string Quality { get; set; } = string.Empty;
    }
}
