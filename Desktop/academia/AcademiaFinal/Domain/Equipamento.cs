using System.ComponentModel.DataAnnotations;

namespace NexusGym.Domain
{
    public class Equipamento
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public string Marca { get; set; } = string.Empty;
        public int Peso { get; set; }
        public DateTime DtCadastro { get; set; } = DateTime.UtcNow;
        public bool Ativo { get; set; } = true;


        public void Atualizar(string nome, string descricao, string marca, int peso)
        {
            this.Nome = nome;
            this.Descricao = descricao;
            this.Marca = marca;
            this.Peso = peso;
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
