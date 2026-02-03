using AutoMapper;
using NexusGym.Application.Dto.Equipamentos;
using NexusGym.Domain.Abstractions.Equipamentos;
using NexusGym.Exceptions;

namespace NexusGym.Application.Services.UseCases.Equipamentos.Queries
{
    public class ObterEquipamentoPorIdUseCase
    {
        private readonly IEquipamentoRepository _equipamentoRepository;
        private readonly IMapper _mapper;

        public ObterEquipamentoPorIdUseCase (IEquipamentoRepository equipamentoRepository, IMapper mapper)
        {
            _equipamentoRepository = equipamentoRepository;
            _mapper = mapper;
        }

        public async Task<EquipamentoResponseDTO?> Execute(int id)
        {
            var equipamento = await _equipamentoRepository.ObterEquipamentoPorId(id);
            if (equipamento == null)
            {
                throw new(NotFoundExceptions.EquipamentoNotFound);
            }
            return _mapper.Map<EquipamentoResponseDTO?>(equipamento);
        }
    }
}
