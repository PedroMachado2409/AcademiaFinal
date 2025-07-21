using System.ComponentModel.DataAnnotations;

namespace NexusGym.Domain
{
    public class FichaDeTreino
    {
        [Key]
        public int Id { get; set; }
        public Cliente Cliente { get; set; }
        public int ClienteId { get; set; }
        public Usuario usuario { get; set; }
        public int UsuarioId { get; set; }
        public DateTime DataCadastro { get; set; } = DateTime.UtcNow;

        public string TipoTreino { get; set; } = string.Empty;

        public bool Ativo { get; set; } = true;
        public List<ItemFichaDeTreino> Itens { get; set; } = new List<ItemFichaDeTreino>();

        public void Ativar()
        {
            this.Ativo = true;
        }

        public void Inativar()
        {
            this.Ativo = false;
        }


    }
}
