using Microsoft.AspNetCore.Mvc;
using NexusGym.Application.Dto;
using NexusGym.Application.Services;

namespace NexusGym.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly ClienteService _service;

        public ClienteController(ClienteService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> ListarClientes()
        {
            var clientes = await _service.ListarClientes();
            return Ok(clientes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterClientePorId(int id)
        {
            var cliente = await _service.ObterClientePorId(id);
            return Ok(cliente);
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarCliente([FromBody] ClienteDTO clienteDto)
        {
            var cliente = await _service.CadastrarCliente(clienteDto);
            return CreatedAtAction(nameof(ObterClientePorId), new { id = cliente.Id }, cliente);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarCliente(int id, [FromBody] ClienteDTO clienteDto)
        {
            clienteDto.Id = id; 
            var cliente = await _service.AtualizarCliente(clienteDto);
            return Ok(cliente);
        }

        [HttpPut("{id}/ativar")]
        public async Task<IActionResult> AtivarCliente(int id)
        {
             await _service.AtivarCliente(id);
            return NoContent();
        }

        [HttpPut("{id}/inativar")]
        public async Task<IActionResult> InativarCliente(int id)
        {
            await _service.InativarCliente(id);
            return NoContent();
        }


    }
}
