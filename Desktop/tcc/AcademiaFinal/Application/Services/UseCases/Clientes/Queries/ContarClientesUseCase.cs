using NexusGym.Domain.Abstractions;
using NexusGym.Domain.Abstractions.Clientes;

namespace NexusGym.Application.Services.UseCases.Clientes.Queries
{
    public class ContarClientesUseCase
        : IUseCase<int>
    {
        private readonly IClienteRepository _repository;

        public ContarClientesUseCase(IClienteRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> Execute()
        {
            return await _repository.ContarClientes();
        }
    }
}
