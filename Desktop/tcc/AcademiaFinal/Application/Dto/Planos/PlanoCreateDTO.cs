namespace NexusGym.Application.Dto.Planos
{
    public class PlanoCreateDTO
    {
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public double Valor { get; set; }
        public int DuracaoMeses { get; set; }
    }
}
