
using MercadoLivre.Core.Interface.IBac;
using MercadoLivre.Core.Interface.IRepository;
using MercadoLivre.Core.Interface.IServices;
using MercadoLivre.Core.Models;
using MercadoLivre.Core.Models.MLRefreshToken;
using MercadoLivre.Core.Services;

namespace MercadoLivre.Core.Bac
{
    public sealed class MercadoLivreConfigBac : IMercadoLivreConfigBac
    {
        private readonly IMercadoLivreRepository _mercadoLivreRepository;
        private readonly ITokenService _tokenService;

        public MercadoLivreConfigBac(
            IMercadoLivreRepository mercadoLivreRepository, 
            ITokenService tokenService)
        {
            _mercadoLivreRepository = mercadoLivreRepository;
            _tokenService = tokenService;
        }

        /// <summary>
        /// Fetch configuration for authorizations in Mercado Livre
        /// Verifies if the token is out of date and refresh token
        /// </summary>
        /// <param name="applicationId"></param>
        /// <returns>Configuration</returns>
        public async Task<MercadoLivreConfig> FetchMercadoLivreConfig(long applicationId)
        {
            var config = await _mercadoLivreRepository.FetchMercadoLivreConfig(applicationId);

            // Verify if the last update of token is over expiration time (6 hours)
            if (config.LastUpdateToken.AddHours(6) <= DateTime.UtcNow)
            {
                MLRefreshToken refreshedToken = await _tokenService.RefreshTokenAsync(config);

                // this to keep prior setting in case the process fails
                MercadoLivreConfig newConfig = config;
                newConfig.AccessToken = refreshedToken.AccessToken;
                newConfig.RefreshToken = refreshedToken.RefreshToken;
                newConfig.LastUpdateToken = DateTime.UtcNow;

                int result = await UpdateRefreshTokenAsync(newConfig);
                if (result > 0)
                {
                    // must implement a log here
                    return newConfig;
                }
            }

            return config;
        }

        public async Task<MercadoLivreConfig> FetchMercadoLivreConfigByCompany(long companyId)
        {
            return await _mercadoLivreRepository.FetchMercadoLivreConfigByCompany(companyId);
        }

        public async Task<int> UpdateRefreshTokenAsync(MercadoLivreConfig mercadoLivreConfig)
        {
            return await _mercadoLivreRepository.UpdateRefreshTokenAsync(mercadoLivreConfig);
        }
    }
}
