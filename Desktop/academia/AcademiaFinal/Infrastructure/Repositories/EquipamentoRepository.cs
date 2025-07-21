using Microsoft.EntityFrameworkCore;
using NexusGym.Domain;
using NexusGym.Infrastructure.Data;
using System.Diagnostics.Contracts;

namespace NexusGym.Infrastructure.Repositories
{
    public class EquipamentoRepository 
    {
        private readonly AppDbContext _context;

        public EquipamentoRepository(AppDbContext context)
            {
                _context = context;
            }
    
        public async Task<List<Equipamento>> ListarEquipamentos()
        {
            return await _context.Equipamentos.ToListAsync();
        }

        public async Task<Equipamento?> ObterEquipamentoPorId(int id)
        {
            return await _context.Equipamentos.FindAsync(id);
        }

        public async Task<Equipamento?> ObterEquipamentoPorNome(string nome)
        {
            return await _context.Equipamentos.FirstOrDefaultAsync(e => e.Nome == nome);
        }

        public async Task <Equipamento>AdicionarEquipamento(Equipamento equipamento)
        {
            _context.Equipamentos.Add(equipamento);
            await _context.SaveChangesAsync();
            return equipamento;
        }


        public async Task<Equipamento?> AtualizarEquipamento(Equipamento equipamento)
        {
            _context.Update(equipamento);
            await _context.SaveChangesAsync();
            return equipamento;

        }

        public async Task<List<Equipamento>> ObterPorIds(List<int> ids)
        {
            return await _context.Equipamentos
                .Where(e => ids.Contains(e.Id))
                .ToListAsync();
        }

    }
}
