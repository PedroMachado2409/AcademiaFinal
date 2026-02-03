using NexusGym.Domain.Abstractions;
using NexusGym.Domain.Abstractions.Clientes;
using NexusGym.Exceptions;
using NexusGym.Exceptions.Clientes;

namespace NexusGym.Application.Services.UseCases.Clientes.Commands
{
    public class AtivarClienteUseCase
        : IUseCase<int, bool>
    {
        private readonly IClienteRepository _repository;

        public AtivarClienteUseCase(IClienteRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Execute(int id)
        {
            var cliente = await _repository.ObterClientePorId(id)
                ?? throw new(ClientesExceptions.Cliente_NaoEncontrado);

            if (cliente.Ativo == true)
                throw new(ClientesExceptions.Cliente_JaAtivo);

            cliente.Ativar();
            await _repository.AtualizarCliente(cliente);

            return true;
        }
    }
}
