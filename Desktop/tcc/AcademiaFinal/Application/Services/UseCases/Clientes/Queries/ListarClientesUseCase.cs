using AutoMapper;
using NexusGym.Application.Dto.Cliente;
using NexusGym.Domain.Abstractions;
using NexusGym.Domain.Abstractions.Clientes;

namespace NexusGym.Application.Services.UseCases.Clientes.Queries
{
    public class ListarClientesUseCase
        : IUseCase<List<ClienteResponseDTO>>
    {
        private readonly IClienteRepository _repository;
        private readonly IMapper _mapper;

        public ListarClientesUseCase(IClienteRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<ClienteResponseDTO>> Execute()
        {
            var clientes = await _repository.ListarClientes();
            return _mapper.Map<List<ClienteResponseDTO>>(clientes);
        }
    }
}
