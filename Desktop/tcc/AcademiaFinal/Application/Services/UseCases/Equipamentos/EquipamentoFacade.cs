using NexusGym.Application.Dto.Equipamentos;
using NexusGym.Application.Services.UseCases.Equipamentos.Commands;
using NexusGym.Application.Services.UseCases.Equipamentos.Queries;

namespace NexusGym.Application.Services.UseCases.Equipamentos
{
    public class EquipamentoFacade
    {
        private readonly AdicionarEquipamentoUseCase _adicionar;
        private readonly AtualizarEquipamentoUseCase _atualizar;
        private readonly AtivarEquipamentoUseCase _ativar;
        private readonly InativarEquipamentoUseCase _inativar;
        private readonly ListarEquipamentosUseCase _listar;
        private readonly ObterEquipamentoPorIdUseCase _obter;

        public EquipamentoFacade(
            AdicionarEquipamentoUseCase adicionar,
            AtualizarEquipamentoUseCase atualizar,
            AtivarEquipamentoUseCase ativar,
            InativarEquipamentoUseCase inativar,
            ListarEquipamentosUseCase listar,
            ObterEquipamentoPorIdUseCase obter)
        {
            _adicionar = adicionar;
            _atualizar = atualizar;
            _ativar = ativar;
            _inativar = inativar;
            _listar = listar;
            _obter = obter;
        }

        public Task<EquipamentoResponseDTO> Adicionar(EquipamentoCreateDTO dto) => _adicionar.Execute(dto);
        public Task<EquipamentoResponseDTO> Atualizar(EquipamentoUpdateDTO dto) => _atualizar.Execute(dto);
        public Task Ativar(int id) => _ativar.Execute(id);
        public Task Inativar(int id) => _inativar.Execute(id);
        public Task<List<EquipamentoResponseDTO>> Listar() => _listar.Execute();
        public Task<EquipamentoResponseDTO> Obter(int id) => _obter.Execute(id);
    }
}
