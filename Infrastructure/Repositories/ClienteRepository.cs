using Microsoft.EntityFrameworkCore;
using NexusGym.Domain;
using NexusGym.Infrastructure.Data;

namespace NexusGym.Infrastructure.Repositories
{
    public class ClienteRepository
    {

        private readonly AppDbContext _context;

        public ClienteRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Cliente>> ListarClientes()
        {
            return await _context.Clientes.OrderBy(c => c.Id).ToListAsync();
        }

        public async Task<Cliente?> ObterClientePorId(int id)
        {
            return await _context.Clientes.FindAsync(id);
        }

        public async Task<Cliente?> ObterClientePorCpf(string cpf)
        {
            return await _context.Clientes.FirstOrDefaultAsync(c => c.Cpf == cpf);
        }

        public async Task<Cliente> AdicionarCliente(Cliente cliente)
        {
             _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();
            return cliente;
        }

        public async Task<Cliente> AtualizarCliente(Cliente cliente)
        {
             _context.Clientes.Update(cliente);
            await _context.SaveChangesAsync();
            return cliente;
        }

     

        


    }
}
