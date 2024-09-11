
using MercadoLivre.Core.Models;

namespace MercadoLivre.Core.Interface.IRepository
{
    public interface IMercadoLivreRepository
    {
        Task<MercadoLivreConfig> FetchMercadoLivreConfig(long applicationId);

        Task<MercadoLivreConfig> FetchMercadoLivreConfigByCompany(long companyId);

        Task<int> UpdateRefreshTokenAsync(MercadoLivreConfig mercadoLivreConfig);
    }
}
