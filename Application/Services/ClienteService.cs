using AutoMapper;
using NexusGym.Application.Dto;
using NexusGym.Domain;
using NexusGym.Exceptions;
using NexusGym.Infrastructure.Repositories;

namespace NexusGym.Application.Services
{
    public class ClienteService
    {
        private readonly ClienteRepository _repository;
        private readonly IMapper _mapper;

        public ClienteService(ClienteRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<ClienteDTO>> ListarClientes()
        {
            var clientes = await _repository.ListarClientes();
            return _mapper.Map<List<ClienteDTO>>(clientes);
        }

        public async Task<ClienteDTO> ObterClientePorId(int id)
        {
            // Busca o cliente pelo Id, lança exceção caso não encontre
            var cliente = await _repository.ObterClientePorId(id);
            if (cliente == null)
                throw new(NotFoundExceptions.ClienteNotFound);
            return _mapper.Map<ClienteDTO>(cliente);
        }

        public async Task<ClienteDTO> CadastrarCliente(ClienteDTO dto)
        {
            // Mapeia DTO para entidade
            var novoCliente = _mapper.Map<Cliente>(dto);

            // Verifica se já existe cliente com mesmo CPF para evitar duplicidade
            var clienteExistente = await _repository.ObterClientePorCpf(dto.Cpf);
            if (clienteExistente != null)
                throw new(ExceptionsMessage.CpfExiste);

            // Adiciona cliente e salva no banco
            await _repository.AdicionarCliente(novoCliente);
            return _mapper.Map<ClienteDTO>(novoCliente);
        }

        public async Task<ClienteDTO> AtualizarCliente(ClienteDTO dto)
        {
            // Verifica se o cliente existe antes de atualizar
            var clienteExistente = await _repository.ObterClientePorId(dto.Id);
            if (clienteExistente == null)
                throw new(NotFoundExceptions.ClienteNotFound);

            // Garante que não exista outro cliente com o mesmo CPF
            var clienteComCpfExistente = await _repository.ObterClientePorCpf(dto.Cpf);
            if (clienteComCpfExistente != null && clienteComCpfExistente.Id != dto.Id)
                throw new(ExceptionsMessage.CpfExiste);

            // Atualiza os dados do cliente
            var clienteAtualizado = _mapper.Map<Cliente>(dto);
            await _repository.AtualizarCliente(clienteAtualizado);
            return _mapper.Map<ClienteDTO>(clienteAtualizado);
        }

        public async Task InativarCliente(int id)
        {
            // Recupera o cliente e lança exceção se não existir
            var cliente = await ObterClienteOuExcecao(id);

            // Verifica se o cliente já está inativo para evitar operação redundante
            if (!cliente.Ativo)
                throw new(ExceptionsMessage.ClienteInativo);

            // Executa a inativação e atualiza o banco
            cliente.Inativar();
            await _repository.AtualizarCliente(cliente);
        }

        public async Task AtivarCliente(int id)
        {
            // Recupera o cliente e lança exceção se não existir
            var cliente = await ObterClienteOuExcecao(id);

            // Verifica se o cliente já está ativo para evitar operação redundante
            if (cliente.Ativo)
                throw new(ExceptionsMessage.ClienteAtivo);

            // Executa a ativação e atualiza o banco
            cliente.Ativar();
            await _repository.AtualizarCliente(cliente);
        }

        // Método extraído para reutilização e evitar duplicação
        // Busca cliente pelo id e lança exceção se não encontrado
        private async Task<Cliente> ObterClienteOuExcecao(int id)
        {
            var cliente = await _repository.ObterClientePorId(id);
            if (cliente == null)
                throw new(NotFoundExceptions.ClienteNotFound);
            return cliente;
        }
    }
}
