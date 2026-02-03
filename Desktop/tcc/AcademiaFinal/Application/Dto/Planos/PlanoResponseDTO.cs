namespace NexusGym.Application.Dto.Planos
{
    public class PlanoResponseDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public double Valor { get; set; }
        public int DuracaoMeses { get; set; }
        public DateTime DataCadastro { get; set; } 
        public bool Ativo { get; set; } = true;
    }
}
