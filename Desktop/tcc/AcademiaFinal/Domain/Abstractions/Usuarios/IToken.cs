using NexusGym.Domain.Entities;

namespace NexusGym.Domain.Abstractions.Usuarios
{
    public interface IToken
    {
        string GerarToken(Usuario usuario);
    }
}
