
using MercadoLivre.Core.Models.MLMessage;

namespace MercadoLivre.Core.Interface.IServices
{
    public interface IBaseService
    {
        Task<string> GetResourceAsync(string resource, string accessToken);

        Task<string> SendAnswerToMercadoLivreAsync(string jsonContent, string resource, string accessToken);
    }
}
