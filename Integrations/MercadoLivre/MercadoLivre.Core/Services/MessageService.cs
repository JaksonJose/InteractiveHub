
using MercadoLivre.Core.Interface.IServices;
using MercadoLivre.Core.Models.MLMessage;
using MercadoLivre.Core.Models.MLMessagesPack;
using MercadoLivre.Core.Models.MLOrder;
using MercadoLivre.Core.Models.MLProduct;
using MercadoLivre.Core.Models.MLQuestion;
using MercadoLivreTeste.App.Models.MLQuestions;
using System.Text.Json;

namespace MercadoLivre.Core.Services
{
    public sealed class MessageService : IMessageService
    {
        private IBaseService _baseService;

        public MessageService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<MLMessagesPack> FetchMessagesPackAsync(long orderId, long sellerId, string accessToken) 
        { 
            MLMessagesPack response = new();

            try
            {
                string packResource = $"messages/packs/{orderId}/sellers/{sellerId}?tag=post_sale";
                string result = await _baseService.GetResourceAsync(packResource, accessToken);

                JsonSerializerOptions options = new()
                {
                    PropertyNameCaseInsensitive = true
                };

                response = JsonSerializer.Deserialize<MLMessagesPack>(result, options) ?? new();
            }
            catch (Exception ex)
            {

            }

            return response;
        }

        /// <summary>
        /// Fetch the order by id
        /// </summary>
        /// <param name="orderId">Id of the order to be requested to the ML api</param>
        /// <param name="accessToken">Access token for authorization of the ML api</param>
        /// <returns>The order found</returns>
        public async Task<MLOrder> FetchOrdersByIdAsync(long orderId, string accessToken)
        {
            MLOrder response = new();

            try
            {
                string messageResource = $"/orders/{orderId}";
                string result = await _baseService.GetResourceAsync(messageResource, accessToken);

                JsonSerializerOptions options = new()
                {
                    PropertyNameCaseInsensitive = true
                };

                response = JsonSerializer.Deserialize<MLOrder>(result, options) ?? new();                
            }
            catch (Exception ex)
            {
                // TODO: apply log here
            }

            return response;
        }

        /// <summary>
        /// Fetch post sales message by the message id
        /// </summary>
        /// <param name="resource">Resource conatining the message id and route</param>
        /// <param name="accessToken">Access token for the authorization of the ML api</param>
        /// <returns>The Message payload</returns>
        public async Task<MLMessagePayload> FetchPostMessageByIdAsync(string resource, string accessToken)
        {
            MLMessagePayload response = new();

            try
            {
                string messageResource = $"/{resource}?tag=post_sale";
                string result = await _baseService.GetResourceAsync(messageResource, accessToken);

                JsonSerializerOptions options = new()
                {
                    PropertyNameCaseInsensitive = true
                };

                response = JsonSerializer.Deserialize<MLMessagePayload>(result, options) ?? new();
            }
            catch (Exception ex)
            {
                //TODO: Should save a log
            }

            return response;
        }

        /// <summary>
        /// Fetch product by id
        /// </summary>
        /// <param name="productId">Id of the prroduct to be requested to the ML api</param>
        /// <param name="accessToken">Access token for authorization of the ML api</param>
        /// <returns>The product found</returns>
        public async Task<MLProduct> FetchProductByIdAsync(string productId, string accessToken)
        {
            MLProduct response = new();

            try
            {
                string resource = $"/items?ids={productId}";
                string result = await _baseService.GetResourceAsync(resource, accessToken);

                JsonSerializerOptions options = new()
                {
                    PropertyNameCaseInsensitive = true
                };

                var productResponses = JsonSerializer.Deserialize<List<MLProductResponse>>(result, options) ?? new();

                response = productResponses[0].Body;
            }
            catch (Exception ex)
            {
                //TODO: Should save a log
            }

            return response;
        }

        /// <summary>
        /// Fetch question by id
        /// </summary>
        /// <param name="resource">Resource containing the route and id of the question</param>
        /// <param name="accessToken">Access token for authorizations of the ML api</param>
        /// <returns>The question found</returns>
        public async Task<MLQuestion> FetchQuestionByIdAsync(string resource, string accessToken)
        {
            MLQuestion response = new();

            try
            {
                string result = await _baseService.GetResourceAsync(resource, accessToken);

                JsonSerializerOptions options = new()
                {
                    PropertyNameCaseInsensitive = true
                };

                response = JsonSerializer.Deserialize<MLQuestion>(result, options) ?? new();
            }
            catch (Exception ex)
            {
                //TODO: Should save a log
            }

            return response;
        }

        public async Task SendAnswerToMessageAsync(MLAnswerMessage answer, string pathRef, string accessToken)
        {
            //MLQuestion response = new();

            try
            {
                string resource = $"/{pathRef}?tag=post_sale";

                string jsonContent = JsonSerializer.Serialize(answer);

                string result = await _baseService.SendAnswerToMercadoLivreAsync(jsonContent, resource, accessToken);

                JsonSerializerOptions options = new()
                {
                    PropertyNameCaseInsensitive = true
                };

                //response = JsonSerializer.Deserialize<MLQuestion>(result, options) ?? new();
            }
            catch (Exception ex)
            {
                //TODO: Return a badrequest, exception
            }

            //return response;
        }

        public async Task SendAnswerToQuestionAsync(MLAnswerToQuestion answerToQuestion, string accessToken)
        {
            try
            {
                string resource = $"/answer";

                string jsonContent = JsonSerializer.Serialize(answerToQuestion);

                string result = await _baseService.SendAnswerToMercadoLivreAsync(jsonContent, resource, accessToken);

                JsonSerializerOptions options = new()
                {
                    PropertyNameCaseInsensitive = true
                };

                //response = JsonSerializer.Deserialize<MLQuestion>(result, options) ?? new();
            }
            catch (Exception ex)
            {
                //TODO: Return a badrequest, exception
            }
        }
    }
}