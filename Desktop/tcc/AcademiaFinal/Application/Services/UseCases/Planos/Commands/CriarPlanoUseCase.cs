using AutoMapper;
using NexusGym.Application.Dto.Planos;
using NexusGym.Domain.Abstractions.Planos;
using NexusGym.Domain.Entities;

namespace NexusGym.Application.Services.UseCases.Planos.Commands
{
    public class CriarPlanoUseCase
    {
        private readonly IPlanoRepository _repository;
        private readonly IMapper _mapper;

        public CriarPlanoUseCase (IPlanoRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PlanoResponseDTO> Execute(PlanoCreateDTO planoDTO)
        {
            var plano = _mapper.Map<Plano>(planoDTO);
            await _repository.AdicionarPlano(plano);
            return _mapper.Map<PlanoResponseDTO>(plano);
        }
    }
}
