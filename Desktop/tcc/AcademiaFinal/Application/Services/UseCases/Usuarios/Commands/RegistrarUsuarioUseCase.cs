using AutoMapper;
using NexusGym.Application.Dto.Usuarios;
using NexusGym.Application.Mapping;
using NexusGym.Domain.Abstractions.Usuarios;
using NexusGym.Domain.Entities;
using NexusGym.Infrastructure.Security;

namespace NexusGym.Application.Services.UseCases.Usuarios.Commands
{
    public class RegistrarUsuarioUseCase
    {
        private readonly IUsuarioRepository _repository;
        private readonly IMapper _mapper;

        public RegistrarUsuarioUseCase ( IUsuarioRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<UsuarioDTO> Registrar(UsuarioCreateDTO dto)
        {
            var usuarioExistente = await _repository.ObterUsuarioPorEmail(dto.Email);

            if (usuarioExistente != null)
                throw new BadHttpRequestException("Email já cadastrado!");

            var usuario = _mapper.Map<Usuario>(dto);
            usuario.Senha = PasswordHelper.HashPassword(dto.Senha);

            await _repository.CadastrarUsuario(usuario);

            return _mapper.Map<UsuarioDTO>(usuario);
        }
    }
}
