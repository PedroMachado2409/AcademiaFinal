namespace NexusGym.Domain.Entities
{
    public class Plano
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public double Valor { get; set; }
        public int DuracaoMeses { get; set; }
        public DateTime DataCadastro { get; set; } = DateTime.UtcNow;
        public bool Ativo { get; set; } = true;

        protected Plano() { }

        public Plano(string nome, string descricao, double valor, int duracaoMeses)
        {
            Nome = nome;
            Descricao = descricao;
            Valor = valor;
            DuracaoMeses = duracaoMeses;
        }

        public void Atualizar(string nome, string descricao, double valor, int duracaoMeses)
        {
            Nome = nome;
            Descricao = descricao;
            Valor = valor;
            DuracaoMeses = duracaoMeses;
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
