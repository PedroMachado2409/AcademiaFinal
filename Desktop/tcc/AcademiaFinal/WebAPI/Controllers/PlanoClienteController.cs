using Microsoft.AspNetCore.Mvc;
using NexusGym.Application.Dto;
using NexusGym.Application.Services.NovaPasta;

namespace NexusGym.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlanoClienteController : ControllerBase
    {
        private readonly PlanoClienteService _service;
        public PlanoClienteController(PlanoClienteService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> ListarPlanosClientes()
        {
            var planosClientes = await _service.ListarPlanosClientes();
            return Ok(planosClientes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterPlanoClientePorId(int id)
        {
            var planoCliente = await _service.ObterPlanoClientePorId(id);
            return Ok(planoCliente);
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarPlanoCliente([FromBody] PlanoClienteDTO planoClienteDto)
        {
            var novoPlanoCliente = await _service.AdicionarPlanoCliente(planoClienteDto);
            return CreatedAtAction(nameof(ObterPlanoClientePorId), new { id = novoPlanoCliente.Id }, novoPlanoCliente);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarPlanoCliente(int id, [FromBody] PlanoClienteDTO planoClienteDto)
        {
            planoClienteDto.Id = id;
            var planoAtualizado = await _service.AtualizarPlanoCliente(id, planoClienteDto);
            return Ok(planoAtualizado);
        }

        [HttpPut("{id}/inativar")]
        public async Task<IActionResult> InativarPlanoCliente(int id)
        {
            var planoInativado = await _service.InativarPlanoCliente(id);
            return Ok(planoInativado);
        }

        [HttpPut("{id}/ativar")]
        public async Task<IActionResult> AtivarPlanoCliente(int id)
        {
            var planoAtivado = await _service.AtivarPlanoCliente(id);
            return Ok(planoAtivado);

        }
    }
}
