namespace NexusGym.Application.Dto
{
    public class EquipamentoDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public string Marca { get; set; } = string.Empty;
        public int Peso { get; set; }
        public DateTime DtCadastro { get; set; } = DateTime.UtcNow;
        public bool Ativo { get; set; } = true;
    }
}
