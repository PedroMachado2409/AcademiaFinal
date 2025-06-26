using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using NexusGym.Application.Dto;
using NexusGym.Domain;
using NexusGym.Infrastructure.Repositories;
using NexusGym.Infrastructure.Security;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NexusGym.Application.Services
{
    public class AuthService
    {
        private readonly IConfiguration _configuration;
        private readonly UsuarioRepository _repository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public AuthService(IConfiguration configuration, UsuarioRepository repository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _repository = repository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<LoginResponseDTO> Autenticar(LoginDTO dto)
        {
            var usuario = await _repository.ObterUsuarioPorEmail(dto.Email);

            if (usuario == null || !PasswordHelper.VerifyPassword(dto.Senha, usuario.Senha))
            {
                throw new UnauthorizedAccessException("Usuário ou senha inválidos.");
            }

            var token = GerarToken(usuario);
            return new LoginResponseDTO
            {
                Nome = usuario.Nome,
                Email = usuario.Email,
                Token = token
            };
        }

        public async Task<Usuario> Registrar(Usuario usuario)
        {
            var usuarioExistente = await _repository.ObterUsuarioPorEmail(usuario.Email);
            if (usuarioExistente != null)
            {
                throw new InvalidOperationException("Email já cadastrado.");
            }
            usuario.Senha = PasswordHelper.HashPassword(usuario.Senha);
            return await _repository.CadastrarUsuario(usuario);
        }

        public async Task<List<UsuarioDTO>> ListarUsuarios()
        {
            var usuarios = await _repository.ObterTodosUsuarios();
            return _mapper.Map<List<UsuarioDTO>>(usuarios);
        }

        public async Task<Usuario?> ObterUsuarioAutenticadoAsync()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var email = httpContext?.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(email))
                return null;

            return await _repository.ObterUsuarioPorEmail(email); 
        }

        public async Task<int> ObterUsuarioAutenticadoIdAsync()
        {
            var usuario = await ObterUsuarioAutenticadoAsync();
            return usuario?.Id ?? throw new UnauthorizedAccessException("Usuário não autenticado.");
        }


        private string GerarToken(Usuario usuario)
        {
            var chaveSecreta = _configuration["Jwt:Key"];
            var chaveSimetricar = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(chaveSecreta));
            var credenciais = new SigningCredentials(chaveSimetricar, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, usuario.Email),
                new Claim("nome", usuario.Nome),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credenciais
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
