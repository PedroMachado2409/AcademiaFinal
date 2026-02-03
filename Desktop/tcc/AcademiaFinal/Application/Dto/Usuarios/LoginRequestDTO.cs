using System.ComponentModel.DataAnnotations;

namespace NexusGym.Application.Dto.Usuarios
{
    public class LoginRequestDTO
    {
        public string Email { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
    }
}
