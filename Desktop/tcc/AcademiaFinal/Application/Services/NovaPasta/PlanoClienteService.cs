using AutoMapper;
using NexusGym.Application.Dto;
using NexusGym.Domain.Abstractions.Planos;
using NexusGym.Domain.Entities;
using NexusGym.Exceptions;
using NexusGym.Infrastructure.Repositories;

namespace NexusGym.Application.Services.NovaPasta
{
    public class PlanoClienteService
    {
        private readonly PlanoClienteRepository _repository;
        private readonly IPlanoRepository _planoRepository;
        private readonly IMapper _mapper;

        public PlanoClienteService(PlanoClienteRepository repository, IMapper mapper, IPlanoRepository planoRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _planoRepository = planoRepository;
        }

        public async Task<List<PlanoClienteDTO>> ListarPlanosClientes()
        {
            var planoClientes = await _repository.ListarPlanosClientes();
            return _mapper.Map<List<PlanoClienteDTO>>(planoClientes);
        }

        public async Task<PlanoClienteDTO> ObterPlanoClientePorId(int id)
        {
            var planoCliente = await _repository.ObterPlanoClientePorId(id);
            return _mapper.Map<PlanoClienteDTO>(planoCliente);
        }

        public async Task<PlanoClienteDTO> AdicionarPlanoCliente(PlanoClienteDTO planoClienteDto)
        {
            var planoCliente = _mapper.Map<PlanoCliente>(planoClienteDto);

            var plano = await _planoRepository.ObterPlanoPorId(planoCliente.PlanoId);
            if (plano == null)
            {
                throw new Exception(NotFoundExceptions.PlanoNotFound);
            }

            if (!plano.Ativo)
            {
                throw new Exception(ExceptionsMessage.PlanoInativado);
            }

            var dataInicio = planoCliente.DataInicio != default ? planoCliente.DataInicio : DateTime.UtcNow;
            planoCliente.DataInicio = dataInicio;

            planoCliente.DataFim = dataInicio.AddMonths(plano.DuracaoMeses);

            var novoPlanoCliente = await _repository.AdicionarPlanoCliente(planoCliente);
            return _mapper.Map<PlanoClienteDTO>(novoPlanoCliente);
        }

        public async Task<PlanoClienteDTO?> AtualizarPlanoCliente(int id, PlanoClienteDTO planoClienteDto)
        {
            var planoClienteExistente = await _repository.ObterPlanoClientePorId(id);
            if (planoClienteExistente.PlanoId != planoClienteDto.PlanoId)
            {
                var novoPlano = await _planoRepository.ObterPlanoPorId(planoClienteDto.PlanoId);
                if (novoPlano == null)
                    throw new Exception(NotFoundExceptions.PlanoNotFound);

                if (!novoPlano.Ativo)
                    throw new Exception(ExceptionsMessage.PlanoInativado);

                planoClienteExistente.PlanoId = planoClienteDto.PlanoId;


                var dataInicio = planoClienteDto.DataInicio != default ? planoClienteDto.DataInicio : DateTime.UtcNow;
                planoClienteExistente.DataInicio = dataInicio;


                planoClienteExistente.DataFim = dataInicio.AddMonths(novoPlano.DuracaoMeses);
            }

            planoClienteExistente.Ativo = planoClienteDto.Ativo;

            var planoAtualizado = await _repository.AtualizarPlanoCliente(planoClienteExistente);
            return _mapper.Map<PlanoClienteDTO>(planoAtualizado);
        }

        public async Task<PlanoClienteDTO?> InativarPlanoCliente(int id)
        {
            var planoClienteExistente = await _repository.ObterPlanoClientePorId(id);
            if (planoClienteExistente == null)
            {
                throw new(NotFoundExceptions.PlanoClienteNotFound);
            }
            planoClienteExistente.Inativar();
            var planoInativado = await _repository.AtualizarPlanoCliente(planoClienteExistente);
            return _mapper.Map<PlanoClienteDTO>(planoInativado);
        }

        public async Task<PlanoClienteDTO?> AtivarPlanoCliente(int id)
        {
            var planoClienteExistente = await _repository.ObterPlanoClientePorId(id);
            if (planoClienteExistente == null)
            {
                throw new(NotFoundExceptions.PlanoClienteNotFound);
            }
            planoClienteExistente.Ativo = true;
            var planoAtivado = await _repository.AtualizarPlanoCliente(planoClienteExistente);
            return _mapper.Map<PlanoClienteDTO>(planoAtivado);

        }
    }
}
