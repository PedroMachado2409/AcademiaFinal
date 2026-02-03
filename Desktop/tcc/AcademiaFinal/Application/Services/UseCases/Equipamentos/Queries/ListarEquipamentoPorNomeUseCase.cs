using AutoMapper;
using NexusGym.Application.Dto.Equipamentos;
using NexusGym.Domain.Abstractions.Equipamentos;
using NexusGym.Exceptions;


namespace NexusGym.Application.Services.UseCases.Equipamentos.Queries
{
    public class ListarEquipamentoPorNomeUseCase
    {
        private readonly IEquipamentoRepository _equipamentoRepository;
        private readonly IMapper _mapper;

        public ListarEquipamentoPorNomeUseCase (IEquipamentoRepository equipamentoRepository, IMapper mapper)
        {
            _equipamentoRepository = equipamentoRepository;
            _mapper = mapper;
        }

        public async Task<List<EquipamentoResponseDTO>> Execute(string nome)
        {
            var equipamento = await _equipamentoRepository.ObterEquipamentoPorNome(nome);

            if (equipamento == null)
                throw new(NotFoundExceptions.EquipamentoNotFound);

            return _mapper.Map<List<EquipamentoResponseDTO>>(equipamento);
        }
    }

  
}
