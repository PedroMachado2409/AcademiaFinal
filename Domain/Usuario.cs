using System.ComponentModel.DataAnnotations;

namespace NexusGym.Domain
{
    public class Usuario
    {

        [Key]
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
        public DateTime DataCadastro { get; set; } = DateTime.UtcNow;
        public bool Ativo { get; set; } = true;

        public void Atualizar(string novaSenha, string email, bool ativo)
        {
            Senha = novaSenha;
            Email = email;
            Ativo = ativo;
        }
    }
}
