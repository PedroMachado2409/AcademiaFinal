using NexusGym.Domain;
using NexusGym.Infrastructure.Repositories;
using NexusGym.Exceptions;
using NexusGym.Application.Dto;
using AutoMapper;

namespace NexusGym.Application.Services
{
    public class PlanoService
    {
        private readonly PlanoRepository _repository;
        private readonly IMapper _mapper;

        public PlanoService(PlanoRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<PlanoDTO>> ObterTodosOsPlanos()
        {
             var planos = await _repository.ObterTodosOsPlanos();
            return _mapper.Map<List<PlanoDTO>>(planos);
        }

        public async Task<PlanoDTO> ObterPlanoPorId(int id)
        {
            var plano = await _repository.ObterPlanoPorId(id);
            if (plano == null)
                throw new Exception(NotFoundExceptions.PlanoNotFound);

            return _mapper.Map<PlanoDTO>(plano);
        }

        public async Task<PlanoDTO> AdicionarPlano(PlanoDTO planoDTO)
        {
            var plano = _mapper.Map<Plano>(planoDTO);
            await _repository.AdicionarPlano(plano);
            return _mapper.Map<PlanoDTO>(plano);
        }

        public async Task<PlanoDTO> AtualizarPlano(PlanoDTO planoDto)
        {
            var planoExistente = await _repository.ObterPlanoPorId(planoDto.Id);

            if (planoExistente == null)
                throw new Exception(NotFoundExceptions.PlanoNotFound);

            var planoAtualizado = _mapper.Map<Plano>(planoDto);
            planoExistente.Atualizar(planoAtualizado.Nome, planoAtualizado.Descricao, planoAtualizado.Valor, planoAtualizado.DuracaoMeses);

            await _repository.AtualizarPlano(planoExistente);
            return _mapper.Map<PlanoDTO>(planoExistente);
        }

        public async Task InativarPlano(int id)
        {
            var plano = await _repository.ObterPlanoPorId(id);

            if (plano == null)
                throw new (NotFoundExceptions.PlanoNotFound);

            if (!plano.Ativo)
                throw new (ExceptionsMessage.PlanoInativado);

            plano.Inativar();
            await _repository.AtualizarPlano(plano);
        }

        public async Task AtivarPlano(int id)
        {
            var plano = await _repository.ObterPlanoPorId(id);

            if (plano == null)
                throw new (NotFoundExceptions.PlanoNotFound);

            if (plano.Ativo)
                throw new (ExceptionsMessage.PlanoAtivado);

            plano.Ativar();
            await _repository.AtualizarPlano(plano);
        }
    }
}
