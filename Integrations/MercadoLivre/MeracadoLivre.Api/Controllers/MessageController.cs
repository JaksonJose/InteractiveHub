using CrossCutting.Models;
using MercadoLivre.Core.Interface.IBac;
using MercadoLivre.Core.Interface.IServices;
using MercadoLivre.Core.Models;
using MercadoLivre.Core.Models.MLMessage;
using MercadoLivre.Core.Models.MLMessagesPack;
using MercadoLivre.Core.Models.MLOrder;
using MercadoLivre.Core.Models.MLQuestion;
using MercadoLivreTeste.App.Models.MLQuestions;
using Microsoft.AspNetCore.Mvc;
using System;

namespace MeracadoLivre.Api.Controllers
{
    [ApiController]
    [Route("api/mercadolivre/")]
    public class MessageController : ControllerBase
    {
        private readonly IMercadoLivreConfigBac _configBac;
        private readonly IMessageService _messageService;

        public MessageController(IMercadoLivreConfigBac configBac, IMessageService messageService)
        {
            _configBac = configBac;
            _messageService = messageService;
        }

        [HttpPost("message")]
        public async Task<IActionResult> SendMessageToMercadoLivre(LeadMessage leadMessage)
        {
            MercadoLivreConfig config = await _configBac.FetchMercadoLivreConfigByCompany(leadMessage.CompanyId);

            List<string> uriSplited = leadMessage.PathReference.Split("/").ToList();
            int packsIndex = uriSplited.FindIndex(x => x.Equals("packs"));
            int sellersIndex = uriSplited.FindIndex(x => x.Equals("sellers"));
            
            string sellerIdWithQuery = uriSplited[sellersIndex + 1];

            // Remove ?..
            string sellerId = sellerIdWithQuery.Split('?')[0];
            long orderId = Convert.ToInt64(uriSplited[packsIndex + 1]);

            MLOrder order = await _messageService.FetchOrdersByIdAsync(orderId, config.AccessToken);

            string resource = leadMessage.PathReference;

            MLAnswerMessage answer = new()
            {
                From = new()
                {
                    UserId = Convert.ToInt64(sellerId),
                },
                To = new()
                {
                    UserId = order.Buyer.Id,
                },
                Text = leadMessage.MessageBody,
            };

            await _messageService.SendAnswerToMessageAsync(answer, resource, config.AccessToken);
            // TODO: Implament bad request

            return Ok();
        }

        [HttpPost("answer")]
        public async Task<IActionResult> SendAnswerToMercadoLivre(Question question) 
        {
            MercadoLivreConfig config = await _configBac.FetchMercadoLivreConfigByCompany(question.CompanyId);

            MLAnswerToQuestion mlQuestion = new()
            {
                QuestionId = question.PortalQuestionId,
                Text = question.Message,
            };

            await _messageService.SendAnswerToQuestionAsync(mlQuestion, config.AccessToken);

            // if something goes wrond return badrequest

            return Ok();
        }
    }
}
