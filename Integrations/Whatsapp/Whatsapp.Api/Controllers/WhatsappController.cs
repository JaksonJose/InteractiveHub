using Whatsapp.Core.Interfaces.IServices;
using Whatsapp.Core.Models;
using Whatsapp.Core.Models.Receive;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Whatsapp.Core.Interfaces.IBac;
using whatsapp.Core.Interfaces.IBac;
using CrossCutting.Models;
using whatsapp.Core.Response;


namespace Whatsapp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WhatsappController : ControllerBase
    {
        private readonly IWhatsappService _whatsappService;
        private readonly IWhatsappConfigBac _whatsappConfigBac;
        private readonly IWhatsappSendMessageBac _whatsappSendMessageBac;

        public WhatsappController(IWhatsappService whatsappService, IWhatsappConfigBac whatsappConfigBac, IWhatsappSendMessageBac whatsappSendMessageBac)
        {
            _whatsappService = whatsappService;
            _whatsappConfigBac = whatsappConfigBac;
            _whatsappSendMessageBac = whatsappSendMessageBac;
        }

        [AllowAnonymous]
        [HttpPost("ReceiveMessage")]
        public async Task<IActionResult> ReceiveMessageAsync(WhatsappPayLoad whatsappPayLoad)
        {
            string phoneNumberId = whatsappPayLoad.Entry[0].Changes[0].Value.Metadata.PhoneNumberId;

            // Get the company based on the whatsappConfig
            WhatsappConfig whatsappConfig = await _whatsappConfigBac.FetchWhatsappConfigByPhoneNumberIdAsync(phoneNumberId);

            await _whatsappSendMessageBac.SendMessageToLeadsManagerAsync(whatsappPayLoad, whatsappConfig.CompanyId);
    
            return Ok();
        }

        [AllowAnonymous]
        [HttpGet("ReceiveMessage")]
        public async Task<IActionResult> ReceiveMessageAsync()
        {
            string? mode = HttpContext.Request.Query["hub.mode"];
            string? challenge = HttpContext.Request.Query["hub.challenge"];
            string? verifyToken = HttpContext.Request.Query["hub.verify_token"];

            int challengeConverted = Convert.ToInt32(challenge);

            // if the operation is OK, return the whatsapp challange      
            return Ok(challengeConverted);
        }

        [HttpPost("SendMessage")]
        [AllowAnonymous]
        public async Task<IActionResult> SendMessageAsync(LeadMessage request)
        {
            JsonResponse response = await _whatsappService.SendMessageToWhatsappAsync(request);

            return Ok(response);
        }
    }  
}
