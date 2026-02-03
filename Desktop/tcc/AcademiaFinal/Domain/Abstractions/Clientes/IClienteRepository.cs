using NexusGym.Domain.Entities;

namespace NexusGym.Domain.Abstractions.Clientes
{
    public interface IClienteRepository
    {
        Task<List<Cliente>> ListarClientes();
        Task<Cliente?> ObterClientePorId(int id);
        Task<Cliente?> ObterClientePorCpf(string cpf);
        Task AdicionarCliente(Cliente cliente);
        Task AtualizarCliente(Cliente cliente);
        Task<int> ContarClientes();
    }
}
