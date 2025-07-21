using AutoMapper;
using Moq;
using NexusGym.Application.Dto;
using NexusGym.Domain;
using NexusGym.Exceptions;
using NexusGym.Infrastructure.Interface;
using NexusGym.Infrastructure.Repositories;
using Xunit;

namespace NexusGym.Application.Services
{
    public class ClienteService
    {
        private readonly IClienteRepository _repository;
        private readonly IMapper _mapper;

        public ClienteService(IClienteRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<ClienteDTO>> ListarClientes()
        {
            var clientes = await _repository.Listar();
            return _mapper.Map<List<ClienteDTO>>(clientes);
        }

        public async Task<ClienteDTO> ObterClientePorId(int id)
        {

            var cliente = await _repository.ObterPorId(id);
            if (cliente == null)
                throw new(NotFoundExceptions.ClienteNotFound);
            return _mapper.Map<ClienteDTO>(cliente);
        }

        public async Task<ClienteDTO> CadastrarCliente(ClienteDTO dto)
        {

            var novoCliente = _mapper.Map<Cliente>(dto);

            var clienteExistente = await _repository.ObterClientePorCpf(dto.Cpf);
            if (clienteExistente != null)
                throw new(ExceptionsMessage.CpfExiste);


            await _repository.Adicionar(novoCliente);
            return _mapper.Map<ClienteDTO>(novoCliente);
        }

        public async Task<ClienteDTO> AtualizarCliente(ClienteDTO dto)
        {

            var clienteExistente = await _repository.ObterPorId(dto.Id);
            if (clienteExistente == null)
                throw new(NotFoundExceptions.ClienteNotFound);


            var clienteComCpfExistente = await _repository.ObterClientePorCpf(dto.Cpf);
            if (clienteComCpfExistente != null && clienteComCpfExistente.Id != dto.Id)
                throw new(ExceptionsMessage.CpfExiste);


            var clienteAtualizado = _mapper.Map<Cliente>(dto);
            await _repository.Atualizar(clienteAtualizado);
            return _mapper.Map<ClienteDTO>(clienteAtualizado);
        }

        public async Task InativarCliente(int id)
        {

            var cliente = await ObterClienteOuExcecao(id);

            if (!cliente.Ativo)
                throw new(ExceptionsMessage.ClienteInativo);

            cliente.Inativar();
            await _repository.Atualizar(cliente);
        }

        public async Task AtivarCliente(int id)
        {

            var cliente = await ObterClienteOuExcecao(id);

            if (cliente.Ativo)
                throw new(ExceptionsMessage.ClienteAtivo);
            cliente.Ativar();
            await _repository.Atualizar(cliente);
        }

        private async Task<Cliente> ObterClienteOuExcecao(int id)
        {
            var cliente = await _repository.ObterPorId(id);
            if (cliente == null)
                throw new(NotFoundExceptions.ClienteNotFound);
            return cliente;
        }

        
    }
}
