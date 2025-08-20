using Application.Interfaces;
using Application.Models.Dto;
using Application.Models.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Application.Models.Dto.BattleTableDto;
using static Application.Models.Request.CreateBattleDto;

namespace ForgottenEmpire.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BattleController : ControllerBase
    {
        private readonly IBattleService _battleService;
        public BattleController(IBattleService battleService)
        {
            _battleService = battleService ?? throw new ArgumentNullException(nameof(battleService));
        }
        [HttpGet]
        public async Task<ActionResult<List<BattleTableDto>>> Get(CancellationToken ct)
        {
            try
            {
                var battles = await _battleService.GetAllBattles(ct);
                return Ok(battles);
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocurrio un error al obtener las Batallas");
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<BattleDetailDto>> GetBattleDetailById(int id, CancellationToken ct)
        {
            try
            {
                var battle = await _battleService.GetBattleById(id, ct);
                if (battle == null)
                    return NotFound($"No se encontro la Batalla con id {id}");

                return Ok(battle);
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocurrio un error al obtener la Batalla");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBattleDto dto, CancellationToken ct)
        {
            try
            {
                var newBattle = await _battleService.CreateBattle(dto, ct);
                return Ok(newBattle); // Sin CreatedAtAction, como en Age
            }
            catch (Exception)
            {
                return StatusCode(500, new
                {
                    error = "ErrorInterno",
                    mensaje = "Tuvimos un problema. Vuelve a intentarlo."
                });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] UpdateBattleDto updateBattleDto, CancellationToken ct)
        {
            try
            {
                var existingBattle = await _battleService.GetBattleById(id, ct);
                if (existingBattle == null)
                    return NotFound($"No se encontro la Batalla con ID: {id}");

                await _battleService.UpdateBattle(id, updateBattleDto, ct);
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocurrio un error al actualizar la Batalla");
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken ct)
        {
            try
            {
                var existingBattle = await _battleService.GetBattleById(id, ct);
                if (existingBattle == null)
                    return NotFound($"No se encontro la Batalla con ID: {id}");

                await _battleService.DeleteBattle(id, ct);
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocurrio un error al eliminar la Batalla");
            }
        }
    }
}