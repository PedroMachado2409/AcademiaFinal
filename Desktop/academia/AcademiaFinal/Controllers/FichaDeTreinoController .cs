using Microsoft.AspNetCore.Mvc;
using NexusGym.Application.Dto;
using NexusGym.Application.Services;

namespace NexusGym.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FichaDeTreinoController : ControllerBase
    {
        private readonly FichaDeTreinoService _service;

        public FichaDeTreinoController(FichaDeTreinoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            var fichas = await _service.ListarFichas();
            return Ok(fichas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterPorId(int id)
        {
            var ficha = await _service.ObterPorId(id);
            return Ok(ficha);
        }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] FichaDeTreinoDTO dto)
        {
            var ficha = await _service.CriarFicha(dto);
            return CreatedAtAction(nameof(ObterPorId), new { id = ficha.Id }, ficha);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, [FromBody] FichaDeTreinoDTO dto)
        {
            dto.Id = id;
            var ficha = await _service.AtualizarFicha(dto);
            return Ok(ficha);
        }

        [HttpPut("{id}/ativar")]
        public async Task<IActionResult> Ativar(int id)
        {
            var ficha = await _service.Ativar(id);
            return Ok(ficha);
        }

        [HttpPut("{id}/desativar")]
        public async Task<IActionResult> Desativar(int id)
        {
            var ficha = await _service.Desativar(id);
            return Ok(ficha);
        }
    }
}
