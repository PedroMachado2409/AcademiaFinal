using AutoMapper;
using NexusGym.Application.Dto.Cliente;
using NexusGym.Domain.Abstractions;
using NexusGym.Domain.Abstractions.Clientes;
using NexusGym.Exceptions;

namespace NexusGym.Application.Services.UseCases.Clientes.Commands
{
    public class AtualizarClienteUseCase
        : IUseCase<ClienteUpdateDTO, ClienteResponseDTO>
    {
        private readonly IClienteRepository _repository;
        private readonly IMapper _mapper;

        public AtualizarClienteUseCase(IClienteRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ClienteResponseDTO> Execute(ClienteUpdateDTO dto)
        {
            var cliente = await _repository.ObterClientePorId(dto.Id)
                ?? throw new(NotFoundExceptions.ClienteNotFound);

            var clienteComCpfExistente = await _repository.ObterClientePorCpf(dto.Cpf);
            if (clienteComCpfExistente != null && clienteComCpfExistente.Id != dto.Id)
                throw new(ExceptionsMessage.CpfExiste);

            cliente.Atualizar(dto.Nome, dto.Email, dto.Telefone, dto.Cpf);

            await _repository.AtualizarCliente(cliente);

            return _mapper.Map<ClienteResponseDTO>(cliente);
        }
    }
}
