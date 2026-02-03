namespace NexusGym.Domain.Entities
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

        protected Cliente() { }

        public Cliente(string nome, string email, string cpf, string telefone)
        {
            Atualizar(nome, email, telefone, cpf);
            DataCadastro = DateTime.UtcNow;
            Ativo = true;
        }

        public void Atualizar(string nome, string email, string telefone, string cpf)
        {
            Nome = nome;
            Email = email;
            Telefone = telefone;
            Cpf = cpf;
        }
        public void Inativar() => Ativo = false;
        public void Ativar() => Ativo = true;
    }
}
