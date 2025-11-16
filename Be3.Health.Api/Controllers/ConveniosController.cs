using Be3.Health.Application.Dtos;
using Be3.Health.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Be3.Health.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class conveniosController : ControllerBase
    {
        private readonly IConvenioService _convenioService;

        public conveniosController(IConvenioService convenioService)
        {
            _convenioService = convenioService;
        }

        // GET /api/convenios
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ConvenioResponseDto>>> Get()
        {
            var convenios = await _convenioService.ListarAtivosAsync();
            return Ok(convenios);
        }
    }
}
