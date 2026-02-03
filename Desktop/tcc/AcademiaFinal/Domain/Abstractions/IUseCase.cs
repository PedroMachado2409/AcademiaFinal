namespace NexusGym.Domain.Abstractions
{
    public interface IUseCase { }

    public interface IUseCase<TResponse> : IUseCase
    {
        Task<TResponse> Execute();
    }

    public interface IUseCase<TRequest, TResponse> : IUseCase
    {
        Task<TResponse> Execute(TRequest request);
    }
}
