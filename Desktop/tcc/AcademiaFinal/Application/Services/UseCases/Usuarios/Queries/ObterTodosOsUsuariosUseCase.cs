using AutoMapper;
using NexusGym.Application.Dto.Usuarios;
using NexusGym.Domain.Abstractions.Usuarios;


namespace NexusGym.Application.Services.UseCases.Usuarios.Queries
{
    public class ObterTodosOsUsuariosUseCase
    {
        private readonly IUsuarioRepository _repository;
        private readonly IMapper _mapper;

        public ObterTodosOsUsuariosUseCase (IUsuarioRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<UsuarioDTO>> Execute()
        {
            var usuarios = await _repository.ObterTodosUsuarios();
            return _mapper.Map<List<UsuarioDTO>>(usuarios);
        }
    }
}
