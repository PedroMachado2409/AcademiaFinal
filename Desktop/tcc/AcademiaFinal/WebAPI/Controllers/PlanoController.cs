using Microsoft.AspNetCore.Mvc;
using NexusGym.Application.Dto.Planos;
using NexusGym.Application.Services.UseCases.Planos;

namespace NexusGym.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlanoController : ControllerBase
    {

        private readonly PlanosFacade _facade;

        public PlanoController(PlanosFacade facade)
        {
            _facade = facade ?? throw new ArgumentNullException(nameof(facade));
        }

        [HttpGet]
        public async Task<IActionResult> ObterTodosOsPlanos() =>
            Ok(await _facade.Listar());

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterPlanoPorId(int id) =>
            Ok(await _facade.Obter(id));
            

        [HttpPost]
        public async Task<IActionResult> AdicionarPlano([FromBody] PlanoCreateDTO plano)
        {
            var novoPlano = await _facade.Criar(plano);
            return CreatedAtAction(nameof(ObterPlanoPorId), novoPlano);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarPlano(int id, [FromBody] PlanoUpdateDTO dto)
        {
            dto.Id = id; 
            var planoAtualizado = await _facade.Atualizar(dto);
            return Ok(planoAtualizado);
        }


        [HttpPut("inativar/{id}")]
        public async Task<IActionResult> InativarPlano(int id)
        {
            await _facade.Inativar(id);
            return NoContent();
        }

        [HttpPut("ativar/{id}")]
        public async Task<IActionResult> AtivarPlano(int id)
        {
            await _facade.Ativar(id);
            return NoContent();
        }

   

    }
}
