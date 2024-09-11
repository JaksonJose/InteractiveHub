
using MercadoLivre.Core.Models.MLMessage;
using MercadoLivre.Core.Models.MLMessagesPack;
using MercadoLivre.Core.Models.MLOrder;
using MercadoLivre.Core.Models.MLProduct;
using MercadoLivre.Core.Models.MLQuestion;
using MercadoLivreTeste.App.Models.MLQuestions;

namespace MercadoLivre.Core.Interface.IServices
{
    public interface IMessageService
    {
        Task<MLMessagesPack> FetchMessagesPackAsync(long orderId, long sellerId, string accessToken);

        /// <summary>
        /// Fetch the order by id
        /// </summary>
        /// <param name="orderId">Id of the order to be requested to the ML api</param>
        /// <param name="accessToken">Access token for the authorization of the ML api</param>
        /// <returns>The order found</returns>
        Task<MLOrder> FetchOrdersByIdAsync(long orderId, string accessToken);

        /// <summary>
        /// Fetch post sales message by the message id
        /// </summary>
        /// <param name="resource">Resource conatining the message id and route</param>
        /// <param name="accessToken">Access token for the authorization of the ML api</param>
        /// <returns>The Message payload</returns>
        Task<MLMessagePayload> FetchPostMessageByIdAsync(string resource, string accessToken);

        /// <summary>
        /// Fetch product by id
        /// </summary>
        /// <param name="productId">Id of the prroduct to be requested to the ML api</param>
        /// <param name="accessToken">Access token for the authorization of the ML api</param>
        /// <returns>The product found</returns>
        Task<MLProduct> FetchProductByIdAsync(string productId, string accessToken);

        /// <summary>
        /// Fetch question by id
        /// </summary>
        /// <param name="resource">Resource containing the route and id of the question</param>
        /// <param name="accessToken">Access token for the authorizations of the ML api</param>
        /// <returns>The question found</returns>
        Task<MLQuestion> FetchQuestionByIdAsync(string resource, string accessToken);

        Task SendAnswerToMessageAsync(MLAnswerMessage answer, string pathRef, string accessToken);

        Task SendAnswerToQuestionAsync(MLAnswerToQuestion answerToQuestion, string accessToken);
    }
}
