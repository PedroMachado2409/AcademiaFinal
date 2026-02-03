using Microsoft.AspNetCore.Mvc;
using NexusGym.Application.Dto.Cliente;
using NexusGym.Application.Services.UseCases.Clientes.Commands;
using NexusGym.Application.Services.UseCases.Clientes.Queries;

namespace NexusGym.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly ListarClientesUseCase _listarClientes;
        private readonly ObterClientePorIdUseCase _obterCliente;
        private readonly CadastrarClienteUseCase _cadastrarCliente;
        private readonly AtualizarClienteUseCase _atualizarCliente;
        private readonly AtivarClienteUseCase _ativarCliente;
        private readonly InativarClienteUseCase _inativarCliente;
        private readonly ContarClientesUseCase _contarClientes;

        public ClienteController(
            ListarClientesUseCase listarClientes,
            ObterClientePorIdUseCase obterCliente,
            CadastrarClienteUseCase cadastrarCliente,
            AtualizarClienteUseCase atualizarCliente,
            AtivarClienteUseCase ativarCliente,
            InativarClienteUseCase inativarCliente,
            ContarClientesUseCase contarClientes)
        {
            _listarClientes = listarClientes;
            _obterCliente = obterCliente;
            _cadastrarCliente = cadastrarCliente;
            _atualizarCliente = atualizarCliente;
            _ativarCliente = ativarCliente;
            _inativarCliente = inativarCliente;
            _contarClientes = contarClientes;
        }

        [HttpGet]
        public async Task<IActionResult> ListarClientes()
            => Ok(await _listarClientes.Execute());

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterClientePorId(int id)
            => Ok(await _obterCliente.Execute(id));

        [HttpGet("contar")]
        public async Task<IActionResult> ContarClientes()
            => Ok(await _contarClientes.Execute());

        [HttpPost]
        public async Task<IActionResult> AdicionarCliente([FromBody] ClienteCreateDTO dto)
        {
            var cliente = await _cadastrarCliente.Execute(dto);
            return CreatedAtAction(nameof(ObterClientePorId), new { id = cliente.Id }, cliente);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarCliente(int id, [FromBody] ClienteUpdateDTO dto)
        {
            dto.Id = id;
            return Ok(await _atualizarCliente.Execute(dto));
        }

        [HttpPut("{id}/ativar")]
        public async Task<IActionResult> AtivarCliente(int id)
        {
            await _ativarCliente.Execute(id);
            return NoContent();
        }

        [HttpPut("{id}/inativar")]
        public async Task<IActionResult> InativarCliente(int id)
        {
            await _inativarCliente.Execute(id);
            return NoContent();
        }
    }
}
