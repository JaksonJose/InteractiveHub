
using Microsoft.AspNetCore.Mvc;
using whatsapp.Core.Interfaces.IServices;
using whatsapp.Core.Response;

namespace Whatsapp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TemplatesController : ControllerBase
    {
        private readonly ITemplateService _templateService;
        public TemplatesController(ITemplateService templateService)
        {
            _templateService = templateService;
        }

        [HttpGet]
        public async Task<IActionResult> FetchAllTemplatesAsync(long empresaId)
        {
           var response = await _templateService.FetchTemplatesAsync(empresaId);

            return Ok(response);
        }
    }
}
