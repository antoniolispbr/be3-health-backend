using Be3.Health.Domain.Entities;

namespace Be3.Health.Domain.Interfaces
{
    public interface IConvenioRepository
    {
        Task<IEnumerable<Convenio>> ListarAtivosAsync();
    }
}
