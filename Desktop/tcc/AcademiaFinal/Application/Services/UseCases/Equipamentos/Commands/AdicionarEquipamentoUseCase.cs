using AutoMapper;
using NexusGym.Application.Dto.Equipamentos;
using NexusGym.Domain.Abstractions.Equipamentos;
using NexusGym.Domain.Entities;

namespace NexusGym.Application.Services.UseCases.Equipamentos.Commands
{
    public class AdicionarEquipamentoUseCase
    {
        private readonly IEquipamentoRepository _equipamentoRepository;
        private readonly IMapper _mapper;

        public AdicionarEquipamentoUseCase (IEquipamentoRepository equipamentoRepository, IMapper mapper)
        {
            _equipamentoRepository = equipamentoRepository;
            _mapper = mapper;
        }

        public async Task<EquipamentoResponseDTO> Execute(EquipamentoCreateDTO dto)
        {
            var novoEquipamento = new Equipamento(dto.Nome, dto.Marca, dto.Descricao, dto.Peso, dto.GrupoEquipamento);
            await _equipamentoRepository.AdicionarEquipamento(novoEquipamento);
            return _mapper.Map<EquipamentoResponseDTO>(novoEquipamento);

        }

    }
}
