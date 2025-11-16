using Be3.Health.Application.Dtos;
using Be3.Health.Application.Interfaces;
using Be3.Health.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Be3.Health.Api.Controllers
{
    [ApiController]
    [Route("api/pacientes")]
    public class PacientesController : ControllerBase
    {
        private readonly IPacienteService _pacienteService;

        public PacientesController(IPacienteService pacienteService)
        {
            _pacienteService = pacienteService;
        }

        // GET /api/pacientes
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PacienteResponseDto>>> Get()
        {
            var pacientes = await _pacienteService.ListarAsync();
            return Ok(pacientes);
        }

        // GET /api/pacientes/{id}
        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PacienteResponseDto>> GetById(Guid id)
        {
            var paciente = await _pacienteService.ObterPorIdAsync(id);

            if (paciente is null)
                return NotFound();

            return Ok(paciente);
        }

        // POST /api/pacientes
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PacienteResponseDto>> Create([FromBody] PacienteCreateDto dto)
        {
            try
            {
                var paciente = await _pacienteService.CriarAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = paciente.Id }, paciente);
            }
            catch (BusinessException ex)
            {
                // validações de regra de negócio (dados inválidos, CPF duplicado, etc.)
                return BadRequest(ex.Message);
            }
        }

        // PUT /api/pacientes/{id}
        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(Guid id, [FromBody] PacienteUpdateDto dto)
        {
            try
            {
                var atualizado = await _pacienteService.AtualizarAsync(id, dto);

                if (atualizado is null)
                    return NotFound();

                return NoContent();
            }
            catch (BusinessException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE /api/pacientes/{id} -> soft delete
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var removido = await _pacienteService.RemoverAsync(id);

            if (!removido)
                return NotFound();

            return NoContent();
        }
    }
}
