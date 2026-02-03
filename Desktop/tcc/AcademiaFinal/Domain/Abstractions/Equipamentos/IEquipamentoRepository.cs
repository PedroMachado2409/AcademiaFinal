using NexusGym.Domain.Entities;

namespace NexusGym.Domain.Abstractions.Equipamentos
{
    public interface IEquipamentoRepository
    {
        Task<List<Equipamento>> ListarEquipamentos();
        Task<Equipamento?> ObterEquipamentoPorId(int id);
        Task<Equipamento?> ObterEquipamentoPorNome(string nome); 
        Task<Equipamento> AdicionarEquipamento(Equipamento equipamento);
        Task AtualizarEquipamento(Equipamento equipamento); 
    }

}
