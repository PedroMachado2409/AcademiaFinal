using NexusGym.Domain.Abstractions;
using NexusGym.Domain.Abstractions.Clientes;
using NexusGym.Exceptions;
using NexusGym.Exceptions.Clientes;

namespace NexusGym.Application.Services.UseCases.Clientes.Commands
{
    public class InativarClienteUseCase
        : IUseCase<int, bool>
    {
        private readonly IClienteRepository _repository;

        public InativarClienteUseCase(IClienteRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Execute(int id)
        {
            var cliente = await _repository.ObterClientePorId(id)
                ?? throw new(ClientesExceptions.Cliente_NaoEncontrado);

            if (cliente.Ativo == false)
                throw new(ClientesExceptions.Cliente_JaInativo);

            cliente.Inativar();
            await _repository.AtualizarCliente(cliente);

            return true;
        }
    }
}
