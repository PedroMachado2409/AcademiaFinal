using Microsoft.EntityFrameworkCore;
using NexusGym.Domain.Abstractions.Clientes;
using NexusGym.Domain.Entities;
using NexusGym.Infrastructure.Data;

namespace NexusGym.Infrastructure.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly AppDbContext _context;

        public ClienteRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Cliente>> ListarClientes()
        {
            return await _context.Clientes.AsNoTracking().OrderBy(c => c.Id).ToListAsync();
        }

        public async Task<Cliente?> ObterClientePorId(int id)
        {
            return await _context.Clientes.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Cliente?> ObterClientePorCpf(string cpf)
        {
            return await _context.Clientes.FirstOrDefaultAsync(c => c.Cpf == cpf);
        }

        public async Task AdicionarCliente(Cliente cliente)
        {
            await _context.Clientes.AddAsync(cliente);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarCliente(Cliente cliente)
        {
            _context.Clientes.Update(cliente);
            await _context.SaveChangesAsync();
        }

        public async Task<int> ContarClientes()
        {
            return await _context.Clientes.CountAsync();
        }
    }
}

