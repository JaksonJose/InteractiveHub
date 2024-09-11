
using MercadoLivre.Core.Models;

namespace MercadoLivre.Core.Interface.IBac
{
    public interface IMercadoLivreConfigBac
    {
        /// <summary>
        /// Fetch configuration for authorizations in Mercado Livre
        /// Verifies if the token is out of date and refresh token
        /// </summary>
        /// <param name="applicationId"></param>
        /// <returns>Configuration</returns>
        Task<MercadoLivreConfig> FetchMercadoLivreConfig(long applicationId);

        Task<MercadoLivreConfig> FetchMercadoLivreConfigByCompany(long companyId);

        /// <summary>
        /// Update the token refreshed
        /// </summary>
        /// <param name="mercadoLivreConfig">Mercado Livre configuration updated</param>
        /// <returns></returns>
        Task<int> UpdateRefreshTokenAsync(MercadoLivreConfig mercadoLivreConfig);
    }
}
