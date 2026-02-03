using AutoMapper;
using NexusGym.Application.Dto.Usuarios;
using NexusGym.Domain.Abstractions.Usuarios;
using NexusGym.Exceptions;


namespace NexusGym.Application.Services.UseCases.Usuarios.Queries
{
    public class ObterUsuarioPorIdUseCase
    {
        private readonly IUsuarioRepository _repository;
        private readonly IMapper _mapper;

        public ObterUsuarioPorIdUseCase (IUsuarioRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<UsuarioDTO?> Execute(int id)
        {
            var usuario = await _repository.ObterUsuarioPorId(id);
            if (usuario == null)
                throw new InvalidOperationException("Usuario não encontradio");

            return _mapper.Map<UsuarioDTO>(usuario);

        }

    }
}
