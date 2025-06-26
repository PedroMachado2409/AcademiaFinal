using Microsoft.EntityFrameworkCore;
using NexusGym.Domain;
using NexusGym.Infrastructure.Data;

namespace NexusGym.Infrastructure.Repositories
{
    public class PlanoRepository
    {
        private readonly AppDbContext _context;
        public PlanoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Plano>> ObterTodosOsPlanos()
        {
            return await _context.Planos
                .OrderBy(p => p.Id)
                .ToListAsync();
        }

        public async Task<Plano?> ObterPlanoPorId(int id)
        {
            return await _context.Planos.FindAsync(id);
        }

        public async Task<Plano> AdicionarPlano(Plano plano)
        {
            _context.Planos.Add(plano);
            await _context.SaveChangesAsync();
            return plano;
        }

        public async Task<Plano?> AtualizarPlano(Plano plano)
        {
            _context.Update(plano);
            await _context.SaveChangesAsync();
            return plano;
        }

   

    }
}
