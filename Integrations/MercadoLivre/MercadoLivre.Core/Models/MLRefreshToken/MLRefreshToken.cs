
using System.Text.Json.Serialization;

namespace MercadoLivre.Core.Models.MLRefreshToken
{
    public sealed class MLRefreshToken
    {
        /// <summary>
        /// Access token refreshed
        /// </summary>
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; } = string.Empty;

        /// <summary>
        /// Type of protocol authorization token
        /// </summary>
        [JsonPropertyName("token_type")]
        public string TokenType { get; set; } = string.Empty;

        /// <summary>
        /// The unix timestamp seconds.
        /// 2600 (seconds) represents 6 hours
        /// </summary>
        [JsonPropertyName("expires_in")]
        public long ExpiresIn { get; set; }

        /// <summary>
        /// scope, ruled by mercado livre
        /// </summary>
        [JsonPropertyName("scope")]
        public string Scope { get; set; } = string.Empty;

        /// <summary>
        /// Id of the user (seller)
        /// </summary>
        [JsonPropertyName("user_id")]
        public long UserId { get; set; }

        /// <summary>
        /// Refresh token refreshed
        /// </summary>
        [JsonPropertyName("refresh_token")]
        public string RefreshToken { get; set; } = string.Empty;
    }
}
