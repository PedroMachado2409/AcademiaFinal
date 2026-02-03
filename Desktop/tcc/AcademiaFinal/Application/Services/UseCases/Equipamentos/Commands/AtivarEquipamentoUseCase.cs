using AutoMapper;
using NexusGym.Domain.Abstractions.Equipamentos;
using NexusGym.Exceptions;
using NexusGym.Exceptions.Equipamentos;

namespace NexusGym.Application.Services.UseCases.Equipamentos.Commands
{
    public class AtivarEquipamentoUseCase
    {
        private readonly IEquipamentoRepository _equipamentoRepository;
        private readonly IMapper _mapper;

        public AtivarEquipamentoUseCase (IEquipamentoRepository equipamentoRepository, IMapper mapper)
        {
            _equipamentoRepository = equipamentoRepository;
            _mapper = mapper;
        }

        public async Task Execute(int id)
        {
            var equipamento = await _equipamentoRepository.ObterEquipamentoPorId(id);
            if (equipamento == null)
                throw new(EquipamentoExceptions.Equipamento_NaoEncontrado);
            if (equipamento.Ativo == true)
                throw new(EquipamentoExceptions.Equipamento_JaAtivo);
            equipamento.Ativar();
            await _equipamentoRepository.AtualizarEquipamento(equipamento);
        }
    }
}
