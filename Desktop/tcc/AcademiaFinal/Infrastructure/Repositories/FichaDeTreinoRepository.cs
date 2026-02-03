using Microsoft.EntityFrameworkCore;
using NexusGym.Domain.Entities;
using NexusGym.Infrastructure.Data;

namespace NexusGym.Infrastructure.Repositories
{
    public class FichaDeTreinoRepository
    {
        private readonly AppDbContext _context;

        public FichaDeTreinoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<FichaDeTreino>> ListarFichas()
        {
            return await _context.FichasDeTreino
                .Include(f => f.Cliente)
                .Include(f => f.usuario)
                .Include(f => f.Itens)
                    .ThenInclude(i => i.Equipamento)
                    .OrderBy(f => f.Id)
                .ToListAsync();
        }

        public async Task<FichaDeTreino?> ObterPorId(int id)
        {
            return await _context.FichasDeTreino
                .Include(f => f.Cliente)
                .Include(f => f.usuario)
                .Include(f => f.Itens)
                    .ThenInclude(i => i.Equipamento)
                .FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<FichaDeTreino> AdicionarFicha(FichaDeTreino ficha)
        {
            _context.FichasDeTreino.Add(ficha);
            await _context.SaveChangesAsync();
            return ficha;
        }

        public async Task<FichaDeTreino> AtualizarFicha(FichaDeTreino ficha)
        {
            _context.FichasDeTreino.Update(ficha);
            await _context.SaveChangesAsync();
            return ficha;
        }

    }
}
