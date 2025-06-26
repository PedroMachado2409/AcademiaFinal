using Microsoft.AspNetCore.Mvc;
using NexusGym.Application.Dto;
using NexusGym.Application.Services;
using NexusGym.Domain;

namespace NexusGym.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlanoController : ControllerBase
    {

        private readonly PlanoService _service;
        public PlanoController(PlanoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> ObterTodosOsPlanos()
        {
            var planos = await _service.ObterTodosOsPlanos();
            return Ok(planos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterPlanoPorId(int id)
        {
            var plano = await _service.ObterPlanoPorId(id);
            return Ok(plano);
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarPlano([FromBody] PlanoDTO plano)
        {
            var novoPlano = await _service.AdicionarPlano(plano);
            return CreatedAtAction(nameof(ObterPlanoPorId), new { id = novoPlano.Id }, novoPlano);
        }

        [HttpPut("inativar/{id}")]
        public async Task<IActionResult> InativarPlano(int id)
        {
            await _service.InativarPlano(id);
            return NoContent();
        }

        [HttpPut("ativar/{id}")]
        public async Task<IActionResult> AtivarPlano(int id)
        {
            await _service.AtivarPlano(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarPlano(int id, [FromBody] PlanoDTO planoDto)
        {
            planoDto.Id = id;
            var planoAtualizado = await _service.AtualizarPlano(planoDto);
            return Ok(planoAtualizado);
        }

    }
}
