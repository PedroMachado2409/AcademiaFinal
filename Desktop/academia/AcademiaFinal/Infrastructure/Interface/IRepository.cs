namespace NexusGym.Infrastructure.Interface
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> Listar();
        Task<T?> ObterPorId(int id);
        Task<T> Adicionar(T entidade);
        Task<T> Atualizar(T entidade);
    }

}
