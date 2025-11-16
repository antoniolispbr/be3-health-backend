using Be3.Health.Domain.Entities;
using Be3.Health.Domain.Interfaces;
using Be3.Health.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Be3.Health.Infra.Repositories
{
    public class PacienteRepository : IPacienteRepository
    {
        private readonly AppDbContext _context;

        public PacienteRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Paciente>> ListarAsync()
        {
            // TRAZ TODO MUNDO (se quiser só ativos, pode filtrar por IsActive depois)
            return await _context.Pacientes
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Paciente?> ObterPorIdAsync(Guid id)
        {
            return await _context.Pacientes.FindAsync(id);
        }

        public async Task AdicionarAsync(Paciente paciente)
        {
            await _context.Pacientes.AddAsync(paciente);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(Paciente paciente)
        {
            _context.Pacientes.Update(paciente);
            await _context.SaveChangesAsync();
        }

        public async Task RemoverAsync(Paciente paciente)
        {
            _context.Pacientes.Remove(paciente);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExisteCpfAsync(string cpf, Guid? ignorarId = null)
        {
            var query = _context.Pacientes.AsQueryable()
                .Where(p => p.CPF == cpf);

            if (ignorarId.HasValue)
            {
                query = query.Where(p => p.Id != ignorarId.Value);
            }

            return await query.AnyAsync();
        }
    }
}
