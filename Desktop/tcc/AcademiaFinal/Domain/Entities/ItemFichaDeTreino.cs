using System.ComponentModel.DataAnnotations;

namespace NexusGym.Domain.Entities
{
    public class ItemFichaDeTreino
    {
        [Key]
        public int Id { get; set; }
        public int FichaDeTreinoId { get; set; }
        public FichaDeTreino? FichaDeTreino { get; set; }
        public int EquipamentoId { get; set; }
        public Equipamento Equipamento { get; set; } = null!;
        public int Repeticoes { get; set; }


    }
}
