using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NexusGym.Application.Dto.Usuarios;
using NexusGym.Application.Services.UseCases.Usuarios;
using NexusGym.Application.Services.UseCases.Usuarios.Commands;
using NexusGym.Application.Services.UseCases.Usuarios.Queries;

namespace NexusGym.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class LoginController : ControllerBase
    {
        
        private readonly AutenticarUseCase _autenticarUseCase;
        private readonly RegistrarUsuarioUseCase _registrarUsuarioUseCase;
        private readonly ObterTodosOsUsuariosUseCase _obterTodosOsUsuariosUseCase;
        private readonly ObterUsuarioAutenticadoUseCase _obterUsuarioAutenticadoUse;

        public LoginController(AutenticarUseCase autenticarUseCase, RegistrarUsuarioUseCase registrarUsuarioUseCase,
            ObterTodosOsUsuariosUseCase obterTodosOsUsuariosUseCase, ObterUsuarioAutenticadoUseCase obterUsuarioAutenticadoUseCase)
        {
            _autenticarUseCase = autenticarUseCase;
            _registrarUsuarioUseCase = registrarUsuarioUseCase;
            _obterTodosOsUsuariosUseCase = obterTodosOsUsuariosUseCase;
            _obterUsuarioAutenticadoUse = obterUsuarioAutenticadoUseCase;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO dto)
        {
            var response = await _autenticarUseCase.Execute(dto);
            return Ok(response);
        }

        [HttpPost("Registrar")]
        [AllowAnonymous]
        public async Task<IActionResult> Registrar([FromBody] UsuarioCreateDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var usuarioCadastrado = await _registrarUsuarioUseCase.Registrar(dto);

            return CreatedAtAction(nameof(Login), new { email = usuarioCadastrado.Email, nome = usuarioCadastrado.Nome });

        }

        [HttpGet("Eu")]
        [Authorize]
        public async Task<ActionResult<UsuarioDTO>> ObterUsuarioAutenticado()
        {
            var usuario = await _obterUsuarioAutenticadoUse.Execute();
            return Ok(usuario);
        }

        [HttpGet("TodosUsuarios")]
        [Authorize]
        public async Task<ActionResult<UsuarioDTO>> ListarTodosUsuarios()
        {
            var usuarios = await _obterTodosOsUsuariosUseCase.Execute();
            return Ok(usuarios);
        }

    }
}
