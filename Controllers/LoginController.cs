using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NexusGym.Application.Dto;
using NexusGym.Application.Services;
using NexusGym.Domain;

namespace NexusGym.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class LoginController : ControllerBase
    {
        private readonly AuthService _service;

        public LoginController(AuthService service)
        {
            _service = service;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginDTO dto)
        {
            var response = await _service.Autenticar(dto);
            return Ok(response);
        }

        [HttpPost("Registrar")]
        [AllowAnonymous]
        public async Task<IActionResult> Registrar([FromBody] Usuario usuario)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var usuarioCadastrado = await _service.Registrar(usuario);

            return CreatedAtAction(nameof(Login), new { email = usuarioCadastrado.Email, nome = usuarioCadastrado.Nome });

        }

        [HttpGet("Eu")]
        [Authorize]
        public async Task<ActionResult<UsuarioDTO>> ObterUsuarioAutenticado()
        {
            var usuario = await _service.ObterUsuarioAutenticadoAsync();
            return Ok(usuario);
        }

    }
}
