using Microsoft.AspNetCore.Mvc;
using NexusGym.Application.Dto.Equipamentos;
using NexusGym.Application.Services.UseCases.Equipamentos;

namespace NexusGym.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EquipamentoController : ControllerBase
    {
        private readonly EquipamentoFacade _facade;

        public EquipamentoController(EquipamentoFacade facade)
        {
            _facade = facade ?? throw new ArgumentNullException(nameof(facade));
        }

        [HttpGet]
        public async Task<IActionResult> ListarEquipamentosAsync() =>
            Ok(await _facade.Listar());

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterEquipamentoPorIdAsync(int id) =>
            Ok(await _facade.Obter(id));

        [HttpPost]
        public async Task<IActionResult> AdicionarEquipamentoAsync([FromBody] EquipamentoCreateDTO dto)
        {
            var novoEquipamento = await _facade.Adicionar(dto);
            return CreatedAtAction(nameof(ObterEquipamentoPorIdAsync), new { id = novoEquipamento.Id }, novoEquipamento);
        }

        [HttpPut]
        public async Task<IActionResult> AtualizarEquipamentoAsync([FromBody] EquipamentoUpdateDTO dto) =>
            Ok(await _facade.Atualizar(dto));

        [HttpPatch("ativar/{id}")]
        public async Task<IActionResult> AtivarEquipamentoAsync(int id)
        {
            await _facade.Ativar(id);
            return NoContent();
        }

        [HttpPatch("inativar/{id}")]
        public async Task<IActionResult> InativarEquipamentoAsync(int id)
        {
            await _facade.Inativar(id);
            return NoContent();
        }
    }
}
