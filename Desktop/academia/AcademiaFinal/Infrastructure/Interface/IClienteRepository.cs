using NexusGym.Domain;

namespace NexusGym.Infrastructure.Interface
{
    public interface IClienteRepository : IRepository<Cliente>
    {
        Task<Cliente?> ObterClientePorCpf(string cpf);
    }
}
