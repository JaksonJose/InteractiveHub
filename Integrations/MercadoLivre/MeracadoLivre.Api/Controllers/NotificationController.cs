using MercadoLivre.Core.Interface.IBac;
using MercadoLivre.Core.Interface.IServices;
using MercadoLivre.Core.Models;
using MercadoLivre.Core.Models.MLMessagesPack;
using MercadoLivre.Core.Models.MLOrder;
using MercadoLivre.Core.Models.MLProduct;
using MercadoLivreTeste.App.Models.MLQuestions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MeracadoLivre.Api.Controllers
{
    [Route("api/mercadolivre/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly IMercadoLivreConfigBac _configBac;
        private readonly IMessageBac _messageBac;
        private readonly IQuestionBac _questionBac;
        private readonly IMessageService _messageService;

        public NotificationController(
            IMessageService messageService,
            IMercadoLivreConfigBac configBac,
            IQuestionBac questionBac,
            IMessageBac messageBac)
        {
            _configBac = configBac;
            _questionBac = questionBac;
            _messageService = messageService;
            _messageBac = messageBac;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> GetNotificationAsync(MLNotification notification)
        {
            MercadoLivreConfig config = await _configBac.FetchMercadoLivreConfig(notification.ApplicationId);

            bool isQuestion = notification.Topic.Equals("questions");
            bool isPostMessage = notification.Topic.Equals("messages");
                        
            if (isQuestion)
            {
                // find the question
                MLQuestion question = await _messageService.FetchQuestionByIdAsync(notification.Resource, config.AccessToken);
                
                if (question != null)
                {
                    MLProduct product = await _messageService.FetchProductByIdAsync(question.ItemId, config.AccessToken);

                    // make a method to find the user anyway. Some will have phonenumber and name others not.
 
                    _questionBac.QuestionSendMessageAsync(question, product, config.CompanyId);
                }  
            }

            if (isPostMessage)
            {
                // Request each message sent by the client.
                var messagePayload = await _messageService.FetchPostMessageByIdAsync(notification.Resource, config.AccessToken);

                // Get the cliente name buy the order response
                long buyerId = messagePayload.Messages[0].From.UserId;

                string pathRef = string.Empty;
                string buyerName = string.Empty;      

                if (messagePayload.Messages[0].MessageResources.Count > 0)
                {
                    long orderId = messagePayload.Messages[0].MessageResources.Where(r => r.Name == "packs").Select(r => r.Id).FirstOrDefault();

                    MLOrder order = await _messageService.FetchOrdersByIdAsync(orderId, config.AccessToken);

                    MLMessagesPack pack = await _messageService.FetchMessagesPackAsync(orderId, notification.UserId, config.AccessToken);

                    pathRef = pack.ConversationStatus.Path;

                    string firstName = order.Buyer.FirstName;
                    string lastName = order.Buyer.LastName;                    
                    buyerName = firstName + " " + lastName;
                }

                await _messageBac.SendMensageToLeadManager(messagePayload.Messages[0], buyerName, pathRef, config.CompanyId);
            }

            return Ok();
        }
    }
}