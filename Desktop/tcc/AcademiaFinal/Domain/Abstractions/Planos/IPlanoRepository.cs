using NexusGym.Domain.Entities;

namespace NexusGym.Domain.Abstractions.Planos
{
    public interface IPlanoRepository
    {
        public Task<List<Plano>> ObterTodosOsPlanos();
        public Task<Plano?> ObterPlanoPorId(int id);
        public Task<Plano?> AdicionarPlano (Plano plano);
        public Task AtualizarPlano(Plano plano);
    }
}
