using AutoMapper;
using NexusGym.Application.Dto;
using NexusGym.Domain;
using NexusGym.Exceptions;
using NexusGym.Infrastructure.Repositories;

namespace NexusGym.Application.Services
{
    public class EquipamentoService
    {
        private readonly EquipamentoRepository _equipamentoRepository;
        private readonly IMapper _mapper;

        public EquipamentoService(EquipamentoRepository equipamentoRepository, IMapper mapper)
        {
            _equipamentoRepository = equipamentoRepository;
            _mapper = mapper;
        }

        public async Task<List<EquipamentoDTO>> ListarEquipamentosAsync()
        {
            var equipamentos = await _equipamentoRepository.ListarEquipamentos();
            return _mapper.Map<List<EquipamentoDTO>>(equipamentos);
        }

        public async Task<EquipamentoDTO?> ObterEquipamentoPorIdAsync(int id)
        {
            var equipamento = await _equipamentoRepository.ObterEquipamentoPorId(id);
            if (equipamento == null)
            {
                throw new(NotFoundExceptions.EquipamentoNotFound);
            }
            return _mapper.Map<EquipamentoDTO?>(equipamento);
        }

        public async Task<List<EquipamentoDTO>> ListarEquipamentoPorNome(string nome)
        {
            var equipamento = await _equipamentoRepository.ObterEquipamentoPorNome(nome);

            if (equipamento == null)
                throw new(NotFoundExceptions.EquipamentoNotFound);

            return _mapper.Map<List<EquipamentoDTO>>(equipamento);
        }

        public async Task<EquipamentoDTO> AdicionarEquipamentoAsync(EquipamentoDTO equipamentoDto)
        {
            var equipamento = _mapper.Map<Equipamento>(equipamentoDto);
            var novoEquipamento = await _equipamentoRepository.AdicionarEquipamento(equipamento);
            return _mapper.Map<EquipamentoDTO>(novoEquipamento);
        }

        public async Task<EquipamentoDTO?> AtualizarEquipamentoAsync(int id, EquipamentoDTO equipamentoDto)
        {
            var equipamentoExistente = await _equipamentoRepository.ObterEquipamentoPorId(id);

            if (equipamentoExistente == null)
                throw new(NotFoundExceptions.EquipamentoNotFound);

            equipamentoExistente.Atualizar(equipamentoDto.Nome, equipamentoDto.Descricao, equipamentoDto.Marca, equipamentoDto.Peso);
            var equipamentoAtualizado = await _equipamentoRepository.AtualizarEquipamento(equipamentoExistente);
            return _mapper.Map<EquipamentoDTO?>(equipamentoAtualizado);
        }

        public async Task InativarEquipamento(int id)
        {
            var equipamento = await _equipamentoRepository.ObterEquipamentoPorId(id);
            if (equipamento == null)
            {
                throw new(NotFoundExceptions.EquipamentoNotFound);
            }
            if (equipamento.Ativo == false)
            {
                throw new(ExceptionsMessage.EquipamentoInativo);
            }
            equipamento.Inativar();
            await _equipamentoRepository.AtualizarEquipamento(equipamento);
        }

        public async Task AtivarEquipamento(int id)
        {
            var equipamento = await _equipamentoRepository.ObterEquipamentoPorId(id);
            if (equipamento == null)
            {
                throw new(NotFoundExceptions.EquipamentoNotFound);
            }
            if (equipamento.Ativo == true)
            {
                throw new(ExceptionsMessage.EquipamentoAtivo);
            }
            equipamento.Ativar();
            await _equipamentoRepository.AtualizarEquipamento(equipamento);
        }
    }
}
