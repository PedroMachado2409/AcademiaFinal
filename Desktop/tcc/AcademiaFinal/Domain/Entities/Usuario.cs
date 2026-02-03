using NexusGym.Application.Services.UseCases.Clientes.Commands;
using NexusGym.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace NexusGym.Domain.Entities
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Senha { get; set; } =   string.Empty;
        public UserRole Role { get; set; }
        public DateTime DataCadastro { get; set; } = DateTime.UtcNow;
        public bool Ativo { get; set; } = true;

        protected Usuario () { }

        public Usuario (string Nome, string Email, string Senha, UserRole Role)
        {
            this.Nome = Nome;
            this.Email = Email;
            this.Senha = Senha;
            this.Role = Role;
        }
        public void Atualizar(string novaSenha, string email, bool ativo)
        {
            Senha = novaSenha;
            Email = email;
            Ativo = ativo;
        }

        public void Inativar()
        {
            Ativo = false;
        }

        public void Ativar()
        {
            Ativo = true;
        }
    }
}
