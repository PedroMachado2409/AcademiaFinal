using AutoMapper;
using NexusGym.Application.Dto.Planos;
using NexusGym.Domain.Abstractions.Planos;
using NexusGym.Exceptions;

namespace NexusGym.Application.Services.UseCases.Planos.Queries
{
    public class ObterPlanoPorIdUseCase
    {

        private readonly IPlanoRepository _repository;
        private readonly IMapper _mapper;

        public ObterPlanoPorIdUseCase (IPlanoRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PlanoResponseDTO> Execute(int id)
        {
            var plano = await _repository.ObterPlanoPorId(id);
            if (plano == null)
                throw new Exception(NotFoundExceptions.PlanoNotFound);

            return _mapper.Map<PlanoResponseDTO>(plano);
        }
    }
}
