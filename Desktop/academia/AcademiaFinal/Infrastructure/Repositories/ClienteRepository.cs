using Microsoft.EntityFrameworkCore;
using NexusGym.Domain;
using NexusGym.Infrastructure.Data;
using NexusGym.Infrastructure.Interface;

namespace NexusGym.Infrastructure.Repositories
{
    public class ClienteRepository : IRepository<Cliente>
    {

        private readonly AppDbContext _context;

        public ClienteRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Cliente>> Listar()
        {
            return await _context.Clientes.OrderBy(c => c.Id).ToListAsync();
        }

        public async Task<Cliente?> ObterPorId(int id)
        {
            return await _context.Clientes.FindAsync(id);
        }

        public async Task<Cliente?> ObterClientePorCpf(string cpf)
        {
            return await _context.Clientes.FirstOrDefaultAsync(c => c.Cpf == cpf);
        }

        public async Task<Cliente> Adicionar(Cliente cliente)
        {
             _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();
            return cliente;
        }

        public async Task<Cliente> Atualizar(Cliente cliente)
        {
             _context.Clientes.Update(cliente);
            await _context.SaveChangesAsync();
            return cliente;
        }

     

        


    }
}
