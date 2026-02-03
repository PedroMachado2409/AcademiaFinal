using NexusGym.Domain.Enum;


namespace NexusGym.Domain.Entities
{
    public class Equipamento
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public string Marca { get; set; } = string.Empty;
        public int Peso { get; set; }
        public GrupoEquipamento GrupoEquipamento { get; set; }
        public DateTime DtCadastro { get; set; } = DateTime.UtcNow;
        public bool Ativo { get; set; } = true;

        protected Equipamento() { }

        public Equipamento (string nome, string descricao, string marca, int peso, GrupoEquipamento grupoEquipamento)
        {
            Atualizar(nome, descricao, marca, peso, grupoEquipamento);
            DtCadastro = DateTime.UtcNow;
            Ativo = true;
        }

        public void Atualizar(string nome, string descricao, string marca, int peso, GrupoEquipamento grupoEquipamento)
        {
            this.Nome = nome;
            this.Descricao = descricao;
            this.Marca = marca;
            this.Peso = peso;
            this.GrupoEquipamento = grupoEquipamento;

        }

        public void Inativar() => Ativo = false;
        public void Ativar() => Ativo = true;


    }
}
