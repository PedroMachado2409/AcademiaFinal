namespace NexusGym.Application.Dto
{
    public class PlanoClienteDTO
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public string? NomeCliente { get; set; }
        public int PlanoId { get; set; }
        public string? NomePlano { get; set; }
        public bool Ativo { get; set; } = true;
        public DateTime DataInicio { get; set; }
        public DateTime? DataFim { get; set; }

        
    }
}
