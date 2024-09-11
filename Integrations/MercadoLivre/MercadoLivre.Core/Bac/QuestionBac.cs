
using MercadoLivre.Core.Broker;
using MercadoLivre.Core.Interface.IBac;
using MercadoLivre.Core.Interface.IRepository;
using MercadoLivre.Core.Models;
using MercadoLivre.Core.Models.MLProduct;
using MercadoLivreTeste.App.Models.MLQuestions;

namespace MercadoLivre.Core.Bac
{
    public sealed class QuestionBac : IQuestionBac
    {
        private readonly IMessageBroker _messageBroker;

        public QuestionBac(IMessageBroker messageBroker)
        {
            _messageBroker = messageBroker;
        }

        public void QuestionSendMessageAsync(MLQuestion mlQuestion, MLProduct product, long companyId)
        {
            Question question = ConvertToQuestionResponse(mlQuestion, product, companyId);

            _messageBroker.SendMessage(question, "question-answer");
        }

        private static Question ConvertToQuestionResponse(MLQuestion mlQuestion, MLProduct product, long companyId)
        {
            Question question = new()
            {
                BuyerId = mlQuestion.From.Id,
                CompanyId = companyId,
                DateCreated = mlQuestion.DateCreated,
                ItemId = mlQuestion.ItemId,
                LeadIdentifier = mlQuestion.From.Id.ToString(),
                Message = mlQuestion.Text,
                ProductImageUrl = product.Prictures[0].Url ?? string.Empty,
                ProductLink = product.Permalink,
                PortalQuestionId = mlQuestion.Id,           
            };

            // If the answer is already answered
            if (mlQuestion.Answer != null)
            {
                question.Answer = new()
                {
                    PortalQuestionId = mlQuestion.Id,
                    Message = mlQuestion.Answer.Text,
                    DateCreated = mlQuestion.Answer.DateCreated
                };
            } 
            
            return question;
        }
    }
}
