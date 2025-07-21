namespace NexusGym.Application.Dto
{
    public class FichaDeTreinoDTO
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public int UsuarioId { get; set; }
        public DateTime DataCadastro { get; set; } = DateTime.UtcNow;
        public List<ItemFichaDeTreinoDTO> Itens { get; set; } = new List<ItemFichaDeTreinoDTO>();
        public string TipoTreino { get; set; } = string.Empty;

        public bool Ativo { get; set; } 
    }
}
