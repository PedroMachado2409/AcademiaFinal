using AutoMapper;
using NexusGym.Application.Dto;
using NexusGym.Application.Dto.Equipamentos;
using NexusGym.Domain.Abstractions.Equipamentos;
using NexusGym.Infrastructure.Repositories;

namespace NexusGym.Application.Services.UseCases.Equipamentos.Queries
{
    public class ListarEquipamentosUseCase
    {
        private readonly IEquipamentoRepository _equipamentoRepository;
        private readonly IMapper _mapper;

        public ListarEquipamentosUseCase (IEquipamentoRepository equipamentoRepository, IMapper mapper)
        {
            _equipamentoRepository = equipamentoRepository;
            _mapper = mapper;
        }

        public async Task<List<EquipamentoResponseDTO>> Execute()
        {
            var equipamentos = await _equipamentoRepository.ListarEquipamentos();
            return _mapper.Map<List<EquipamentoResponseDTO>>(equipamentos);
        }
    }
}
