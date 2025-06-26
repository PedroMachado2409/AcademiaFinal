using System.ComponentModel.DataAnnotations;

namespace NexusGym.Domain
{
    public class Plano
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public double Valor { get; set; }
        public int DuracaoMeses { get; set; }
        public DateTime DataCadastro { get; set; } = DateTime.UtcNow;
        public bool Ativo { get; set; } = true;


        public void Atualizar(string nome, string descricao, double valor, int duracaoMeses)
        {
            this.Nome = nome;
            this.Descricao = descricao;
            this.Valor = valor;
            this.DuracaoMeses = duracaoMeses;
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
