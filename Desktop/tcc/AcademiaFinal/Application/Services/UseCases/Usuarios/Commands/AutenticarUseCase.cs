
using NexusGym.Application.Dto.Usuarios;
using NexusGym.Domain.Abstractions.Usuarios;
using NexusGym.Infrastructure.Security;

namespace NexusGym.Application.Services.UseCases.Usuarios.Commands
{
    public class AutenticarUseCase
    {
        private readonly IUsuarioRepository _repository;
        private readonly IToken _token;


        public AutenticarUseCase(IUsuarioRepository repository, IToken token)
        {
            _repository = repository;
            _token = token;
        }

        public async Task<LoginResponseDTO> Execute(LoginRequestDTO dto)
        {
            var usuario = await _repository.ObterUsuarioPorEmail(dto.Email);
            if (usuario == null || !PasswordHelper.VerifyPassword(dto.Senha, usuario.Senha))
                throw new UnauthorizedAccessException("Email ou senha inválidos");

            var token = _token.GerarToken(usuario);

            return new LoginResponseDTO
            {
                Nome = usuario.Nome,
                Email = usuario.Email,
                Token = token
            };
        }
    }
}
