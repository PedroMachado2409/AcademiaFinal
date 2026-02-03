using NexusGym.Domain.Enum;

namespace NexusGym.Application.Dto.Usuarios
{
    public class UsuarioDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public UserRole Role { get; set; }
        public DateTime DataCadastro { get; set; } 
        public bool Ativo { get; set; }
    }
}

