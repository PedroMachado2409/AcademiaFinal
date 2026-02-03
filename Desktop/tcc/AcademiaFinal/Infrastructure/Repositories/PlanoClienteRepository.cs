using Microsoft.EntityFrameworkCore;
using NexusGym.Domain.Entities;
using NexusGym.Infrastructure.Data;

namespace NexusGym.Infrastructure.Repositories
{
    public class PlanoClienteRepository
    {
        private readonly AppDbContext _context;

        public PlanoClienteRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<PlanoCliente>> ListarPlanosClientes()
        {
            return await _context.PlanoClientes
                 .Include(pc => pc.Plano)
                 .Include(pc => pc.Cliente)
                 .OrderBy(pc => pc.Id)
                 .ToListAsync();
        }

        public async Task<PlanoCliente?> ObterPlanoClientePorId(int id)
        {
            return await _context.PlanoClientes
                .Include(pc => pc.Plano)
                .Include(pc => pc.Cliente)
                .FirstOrDefaultAsync(pc => pc.Id == id);
        }

        public async Task<PlanoCliente> AdicionarPlanoCliente(PlanoCliente planoCliente)
        {
            _context.PlanoClientes.Add(planoCliente);
            await _context.SaveChangesAsync();
            return planoCliente;
        }

        public async Task<PlanoCliente?> AtualizarPlanoCliente(PlanoCliente planoCliente)
        {
            _context.PlanoClientes.Update(planoCliente);
            await _context.SaveChangesAsync();
            return planoCliente;
        }

    }
       
}
