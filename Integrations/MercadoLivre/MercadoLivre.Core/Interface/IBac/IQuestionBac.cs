
using MercadoLivre.Core.Models;
using MercadoLivre.Core.Models.MLProduct;
using MercadoLivreTeste.App.Models.MLQuestions;

namespace MercadoLivre.Core.Interface.IBac
{
    public interface IQuestionBac
    {
        void QuestionSendMessageAsync(MLQuestion mlQuestion, MLProduct product, long applicationId);
    }
}
