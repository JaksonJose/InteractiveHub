
using MercadoLivre.Core.Models.MLMessage;
using MercadoLivre.Core.Models.MLOrder;

namespace MercadoLivre.Core.Interface.IBac
{
    public interface IMessageBac
    {
        Task SendMensageToLeadManager(MLMessage message, string buyerName, string pathRef, long companyId);
    }
}
