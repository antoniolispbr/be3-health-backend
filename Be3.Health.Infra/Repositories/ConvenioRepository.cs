using Be3.Health.Domain.Entities;
using Be3.Health.Domain.Interfaces;
using Be3.Health.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Be3.Health.Infra.Repositories
{
    public class ConvenioRepository : IConvenioRepository
    {
        private readonly AppDbContext _context;

        public ConvenioRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Convenio>> ListarAtivosAsync()
        {
            // Lista todos os convênios por enquanto
            return await _context.Convenios
                .OrderBy(c => c.Nome)
                .ToListAsync();
        }
    }
}
