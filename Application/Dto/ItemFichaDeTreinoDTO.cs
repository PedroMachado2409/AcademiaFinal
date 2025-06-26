namespace NexusGym.Application.Dto
{
    public class ItemFichaDeTreinoDTO
    {
        public int Id { get; set; }
        public int EquipamentoId { get; set; }
        public string EquipamentoNome { get; set; } = string.Empty;
        public int Repeticoes { get; set; } = 0;

    }
}
