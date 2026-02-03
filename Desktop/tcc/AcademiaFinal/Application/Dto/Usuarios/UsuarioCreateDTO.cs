using NexusGym.Domain.Enum;

namespace NexusGym.Application.Dto.Usuarios
{
    public class UsuarioCreateDTO
    {
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Senha {  get; set; } = string.Empty;
        public UserRole Role { get; set; }
    }
}
