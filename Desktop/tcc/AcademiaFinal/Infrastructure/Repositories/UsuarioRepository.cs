using Microsoft.EntityFrameworkCore;
using NexusGym.Domain.Abstractions.Usuarios;
using NexusGym.Domain.Entities;
using NexusGym.Infrastructure.Data;

namespace NexusGym.Infrastructure.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly AppDbContext _context;

        public UsuarioRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Usuario?> ObterUsuarioPorEmail(string email)
        {
            return await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<List<Usuario>> ObterTodosUsuarios()
        {
            return await _context.Usuarios.OrderBy(u => u.Id).ToListAsync();

        }

        public async Task<Usuario?> ObterUsuarioPorId(int id)
        {
            return await _context.Usuarios.FindAsync(id);
        }

        public async Task<Usuario> CadastrarUsuario(Usuario novoUsuario)
        {
            await _context.Usuarios.AddAsync(novoUsuario);
            await _context.SaveChangesAsync();
            return novoUsuario;
        }

    }
}
