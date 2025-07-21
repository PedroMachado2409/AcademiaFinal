using Microsoft.AspNetCore.Mvc;
using NexusGym.Application.Dto;
using NexusGym.Application.Services;
using NexusGym.Domain;

namespace NexusGym.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EquipamentoController : ControllerBase
    {

        private readonly EquipamentoService _service;

        public EquipamentoController(EquipamentoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> ListarEquipamentosAsync()
        {
            var equipamentos = await _service.ListarEquipamentosAsync();
            return Ok(equipamentos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterEquipamentoPorIdAsync(int id)
        {
            var equipamento = await _service.ObterEquipamentoPorIdAsync(id);
            return Ok(equipamento);
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarEquipamentoAsync([FromBody] EquipamentoDTO equipamentoDto)
        {
            var novoEquipamento = await _service.AdicionarEquipamentoAsync(equipamentoDto);
            return CreatedAtAction(nameof(ObterEquipamentoPorIdAsync), new { id = novoEquipamento.Id }, novoEquipamento);
        }

    }
}
