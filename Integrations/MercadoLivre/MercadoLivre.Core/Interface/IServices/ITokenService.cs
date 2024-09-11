
using MercadoLivre.Core.Models;
using MercadoLivre.Core.Models.MLRefreshToken;
using MercadoLivreTeste.App.Models.MLQuestions;

namespace MercadoLivre.Core.Interface.IServices
{
    public interface ITokenService
    {
        /// <summary>
        /// Refresh token
        /// </summary>
        /// <param name="config">Configuration data to refresh the token</param>
        /// <returns>The object with refreshed token data</returns>
        Task<MLRefreshToken> RefreshTokenAsync(MercadoLivreConfig config);
    }
}
