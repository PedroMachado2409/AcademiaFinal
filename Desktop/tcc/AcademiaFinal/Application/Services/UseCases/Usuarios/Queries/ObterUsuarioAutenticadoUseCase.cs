using AutoMapper;
using NexusGym.Application.Dto.Usuarios;
using NexusGym.Domain.Abstractions.Usuarios;
using System.Security.Claims;

namespace NexusGym.Application.Services.UseCases.Usuarios.Queries
{
    public class ObterUsuarioAutenticadoUseCase
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;

        public ObterUsuarioAutenticadoUseCase (IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IUsuarioRepository usuarioRepository, IMapper mapper)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
        }
        
        public async Task <UsuarioDTO?> Execute()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var email = httpContext?.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if(string.IsNullOrEmpty(email))
                return null;

            var usuario = await _usuarioRepository.ObterUsuarioPorEmail(email);
            return _mapper.Map<UsuarioDTO>(usuario);

        }

    }
}
