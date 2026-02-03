using NexusGym.Domain.Enum;

namespace NexusGym.Application.Dto.Equipamentos
{
    public class EquipamentoResponseDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public string Marca { get; set; } = string.Empty;
        public int Peso { get; set; }
        public GrupoEquipamento GrupoEquipamento { get; set; }
        public DateTime DtCadastro { get; set; } 
        public bool Ativo { get; set; }
    }
}
