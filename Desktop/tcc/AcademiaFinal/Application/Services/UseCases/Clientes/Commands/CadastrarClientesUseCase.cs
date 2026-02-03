using AutoMapper;
using NexusGym.Application.Dto.Cliente;
using NexusGym.Domain.Entities;
using NexusGym.Domain.Abstractions;
using NexusGym.Domain.Abstractions.Clientes;
using NexusGym.Exceptions.Clientes;

namespace NexusGym.Application.Services.UseCases.Clientes.Commands
{
    public class CadastrarClienteUseCase
        : IUseCase<ClienteCreateDTO, ClienteResponseDTO>
    {
        private readonly IClienteRepository _repository;
        private readonly IMapper _mapper;

        public CadastrarClienteUseCase(IClienteRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ClienteResponseDTO> Execute(ClienteCreateDTO dto)
        {
            var clienteExistente = await _repository.ObterClientePorCpf(dto.Cpf);
            if (clienteExistente != null)
                throw new(ClientesExceptions.Cliente_CpfExistente);

            var novoCliente = new Cliente(dto.Nome, dto.Email, dto.Cpf, dto.Telefone);

            await _repository.AdicionarCliente(novoCliente);

            return _mapper.Map<ClienteResponseDTO>(novoCliente);
        }
    }
}
