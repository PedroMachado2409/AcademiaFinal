using AutoMapper;
using NexusGym.Application.Dto.Planos;
using NexusGym.Domain.Abstractions.Planos;

namespace NexusGym.Application.Services.UseCases.Planos.Queries
{
    public class ListarPlanosUseCase
    {
        private readonly IPlanoRepository _repository;
        private readonly IMapper _mapper;

        public ListarPlanosUseCase (IPlanoRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<PlanoResponseDTO>> Execute()
        {
            var planos = await _repository.ObterTodosOsPlanos();
            return _mapper.Map<List<PlanoResponseDTO>>(planos);
        }
    }
}
