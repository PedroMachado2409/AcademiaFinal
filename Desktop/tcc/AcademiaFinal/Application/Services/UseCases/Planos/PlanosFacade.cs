
using NexusGym.Application.Dto.Planos;
using NexusGym.Application.Services.UseCases.Planos.Commands;
using NexusGym.Application.Services.UseCases.Planos.Queries;

namespace NexusGym.Application.Services.UseCases.Planos
{
    public class PlanosFacade
    {
        private readonly ListarPlanosUseCase _listarPlanosUseCase;
        private readonly ObterPlanoPorIdUseCase _obterPlanoPorIdUseCase;
        private readonly InativarPlanoUseCase _InativarPlanoUseCase;
        private readonly AtivarPlanoUseCase _AtivarPlanoUseCase;
        private readonly AtualizarPlanoUseCase _AtualizarPlanoUseCase;
        private readonly CriarPlanoUseCase _CriarPlanoUseCase;

        public PlanosFacade(
           ListarPlanosUseCase listarPlanosUseCase,
           ObterPlanoPorIdUseCase obterPlanoPorIdUseCase,
           InativarPlanoUseCase inativarPlanoUseCase,
           AtivarPlanoUseCase ativarPlanoUseCase,
           AtualizarPlanoUseCase atualizarPlanoUseCase,
           CriarPlanoUseCase criarPlanoUseCase)
        {
            _listarPlanosUseCase = listarPlanosUseCase;
            _obterPlanoPorIdUseCase = obterPlanoPorIdUseCase;
            _InativarPlanoUseCase = inativarPlanoUseCase;
            _AtivarPlanoUseCase = ativarPlanoUseCase;
            _AtualizarPlanoUseCase = atualizarPlanoUseCase;
            _CriarPlanoUseCase = criarPlanoUseCase;
        }

        public Task<List<PlanoResponseDTO>> Listar() => _listarPlanosUseCase.Execute();
        public Task<PlanoResponseDTO> Obter(int id) => _obterPlanoPorIdUseCase.Execute(id);
        public Task Ativar(int id) => _AtivarPlanoUseCase.Execute(id);
        public Task Inativar(int id) => _InativarPlanoUseCase.Execute(id);
        public Task<PlanoResponseDTO> Criar(PlanoCreateDTO dto) => _CriarPlanoUseCase.Execute(dto);
        public Task<PlanoResponseDTO> Atualizar(PlanoUpdateDTO dto) => _AtualizarPlanoUseCase.AtualizarPlano(dto);


    }
}
