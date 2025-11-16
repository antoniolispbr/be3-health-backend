using Be3.Health.Application.Dtos;
using Be3.Health.Application.Interfaces;
using Be3.Health.Domain.Interfaces;

namespace Be3.Health.Application.Services
{
    public class ConvenioService : IConvenioService
    {
        private readonly IConvenioRepository _repository;

        public ConvenioService(IConvenioRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ConvenioResponseDto>> ListarAtivosAsync()
        {
            var convenios = await _repository.ListarAtivosAsync();

            return convenios
                .OrderBy(c => c.Nome)
                .Select(c => new ConvenioResponseDto
                {
                    Id = c.Id,
                    Nome = c.Nome
                })
                .ToList();
        }
    }
}
