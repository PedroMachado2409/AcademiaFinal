using NexusGym.Domain.Entities;

namespace NexusGym.Domain.Abstractions.Usuarios
{
    public interface IUsuarioRepository
    {
        public Task <List<Usuario>> ObterTodosUsuarios();
        public Task<Usuario?> ObterUsuarioPorEmail(string email);
        public Task<Usuario?> ObterUsuarioPorId(int id);
        public Task<Usuario> CadastrarUsuario(Usuario usuario);

    }
}
