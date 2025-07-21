

namespace NexusGym.Domain
{
    public class Cliente
    {

        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;   

        public string Cpf { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public DateTime DataCadastro { get; set; } = DateTime.UtcNow;
        public bool Ativo { get; set; } = true;

        public void Atualizar(string nome, string email, string telefone, string cpf )
        {
            this.Nome = nome;
            this.Email = email;
            this.Telefone = telefone;
            this.Cpf = cpf;
        }

        public void Inativar()
        {
            this.Ativo = false;
        }

        public void Ativar()
        {
            this.Ativo = true;
        }


    }
}
