using AutoMapper;
using NexusGym.Domain.Abstractions.Planos;
using NexusGym.Exceptions;

namespace NexusGym.Application.Services.UseCases.Planos.Commands
{
    public class InativarPlanoUseCase
    {
        private readonly IPlanoRepository _repository;
        private readonly IMapper _mapper;

        public InativarPlanoUseCase (IPlanoRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Execute(int id)
        {
            var plano = await _repository.ObterPlanoPorId(id);

            if (plano == null)
                throw new(NotFoundExceptions.PlanoNotFound);

            if (!plano.Ativo)
                throw new(ExceptionsMessage.PlanoInativado);

            plano.Inativar();
            await _repository.AtualizarPlano(plano);
        }
    }
}
