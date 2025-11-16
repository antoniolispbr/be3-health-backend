using Be3.Health.Application.Dtos;

namespace Be3.Health.Application.Interfaces
{
    public interface IConvenioService
    {
        Task<IEnumerable<ConvenioResponseDto>> ListarAtivosAsync();
    }
}
