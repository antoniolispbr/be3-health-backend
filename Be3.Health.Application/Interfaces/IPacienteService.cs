using Be3.Health.Application.Dtos;

namespace Be3.Health.Application.Interfaces
{
    public interface IPacienteService
    {
        Task<IEnumerable<PacienteResponseDto>> ListarAsync();
        Task<PacienteResponseDto?> ObterPorIdAsync(Guid id);
        Task<PacienteResponseDto> CriarAsync(PacienteCreateDto dto);
        Task<PacienteResponseDto?> AtualizarAsync(Guid id, PacienteUpdateDto dto);
        Task<bool> RemoverAsync(Guid id);
    }
}
