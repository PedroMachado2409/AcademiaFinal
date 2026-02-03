using AutoMapper;
using NexusGym.Application.Dto.Cliente;
using NexusGym.Domain.Abstractions;
using NexusGym.Domain.Abstractions.Clientes;
using NexusGym.Exceptions;

namespace NexusGym.Application.Services.UseCases.Clientes.Queries
{
    public class ObterClientePorIdUseCase
        : IUseCase<int, ClienteResponseDTO>
    {
        private readonly IClienteRepository _repository;
        private readonly IMapper _mapper;

        public ObterClientePorIdUseCase(IClienteRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ClienteResponseDTO> Execute(int id)
        {
            var cliente = await _repository.ObterClientePorId(id)
                ?? throw new(NotFoundExceptions.ClienteNotFound);

            return _mapper.Map<ClienteResponseDTO>(cliente);
        }
    }
}
