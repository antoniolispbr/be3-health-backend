using Be3.Health.Domain.Entities;

namespace Be3.Health.Domain.Interfaces
{
    public interface IPacienteRepository
    {
        Task<IEnumerable<Paciente>> ListarAsync();
        Task<Paciente?> ObterPorIdAsync(Guid id);
        Task AdicionarAsync(Paciente paciente);
        Task AtualizarAsync(Paciente paciente);
        Task RemoverAsync(Paciente paciente);
        Task<bool> ExisteCpfAsync(string cpf, Guid? ignorarId = null);
    }
}
