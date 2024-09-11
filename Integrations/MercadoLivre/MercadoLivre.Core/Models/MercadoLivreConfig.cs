
namespace MercadoLivre.Core.Models
{
    public sealed class MercadoLivreConfig
    {
        /// <summary>
        /// Identity
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Id of the company
        /// </summary>
        public long CompanyId { get; set; }

        /// <summary>
        /// Application Id, this number is found
        /// in the app created in Mercado Livre app
        /// </summary>
        public long ApplicationId { get; set; }

        /// <summary>
        /// The client secret is found int he app
        /// created in Mercado Livre app
        /// </summary>
        public string ClientSecret { get; set; } = string.Empty;

        /// <summary>
        /// Api Authorization token,
        /// it expires each 6 hours
        /// </summary>
        public string AccessToken { get; set; } = string.Empty;

        /// <summary>
        /// Token to refresh authorization token
        /// used to refresh the Access Token
        /// </summary>
        public string RefreshToken { get; set; } = string.Empty;

        /// <summary>
        /// Last time Access token was updated
        /// </summary>
        public DateTime LastUpdateToken { get; set; }
    }
}
