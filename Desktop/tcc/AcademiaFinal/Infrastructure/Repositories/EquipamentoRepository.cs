using Microsoft.EntityFrameworkCore;
using NexusGym.Domain.Abstractions.Equipamentos;
using NexusGym.Domain.Entities;
using NexusGym.Infrastructure.Data;

namespace NexusGym.Infrastructure.Repositories
{
    public class EquipamentoRepository : IEquipamentoRepository
    {
        private readonly AppDbContext _context;

        public EquipamentoRepository(AppDbContext context)
            {
                _context = context;
            }
    
        public async Task<List<Equipamento>> ListarEquipamentos()
        {
            return await _context.Equipamentos.OrderBy(e => e.Id).ToListAsync();
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


        public async Task AtualizarEquipamento(Equipamento equipamento)
        {
            await _context.SaveChangesAsync();

        }

    }
}
