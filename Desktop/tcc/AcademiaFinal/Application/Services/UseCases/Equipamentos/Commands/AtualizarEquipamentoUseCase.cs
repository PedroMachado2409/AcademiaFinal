using AutoMapper;
using NexusGym.Application.Dto.Equipamentos;
using NexusGym.Domain.Abstractions.Equipamentos;
using NexusGym.Exceptions.Equipamentos;

namespace NexusGym.Application.Services.UseCases.Equipamentos.Commands
{
    public class AtualizarEquipamentoUseCase
    {
        private readonly IEquipamentoRepository _equipamentoRepository;
        private readonly IMapper _mapper;

        public AtualizarEquipamentoUseCase (IEquipamentoRepository equipamentoRepository, IMapper mapper)
        {
            _equipamentoRepository = equipamentoRepository;
            _mapper = mapper;
        }

        public async Task <EquipamentoResponseDTO> Execute(EquipamentoUpdateDTO dto)
        {
            var equipamento = await _equipamentoRepository.ObterEquipamentoPorId(dto.Id);
            if(equipamento == null)
            {
                throw new(EquipamentoExceptions.Equipamento_NaoEncontrado);
            }

            equipamento?.Atualizar(dto.Marca, dto.Descricao, dto.Nome, dto.Peso, dto.GrupoEquipamento);
            await _equipamentoRepository.AtualizarEquipamento(equipamento);
            return _mapper.Map<EquipamentoResponseDTO>(equipamento);
        }
    }
}
