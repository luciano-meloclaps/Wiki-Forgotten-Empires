using Application.Interfaces;
using Application.Models.Dto;
using Application.Models.Request;
using Domain.Entities;
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
        public async Task<ActionResult<List<AgeAccordionDto>>> Get(CancellationToken ct)
        {
            try
            {
                var battles = await _battleService.GetAllBattles(ct);
                return Ok(battles);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocurrió un error al obtener las Batallas: {ex.Message}");
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
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocurrió un error al obtener la Batallas: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<BattleDetailDto>> Create([FromBody] CreateBattleDto dto,CancellationToken ct)
        {
            try
            {
                var newBattle = await _battleService.CreateBattle(dto, ct);
                return Ok(newBattle);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocurrió un error al crear las Batallas: {ex.Message}");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateBattleDto dto,CancellationToken ct)
        {
            try
            {
                var existing = await _battleService.GetBattleById(id, ct);
                if (existing == null)
                    return NotFound($"No se encontró la Batalla con ID {id}");

                await _battleService.UpdateBattle(id, dto, ct);
                return NoContent(); //204 sin contenido para el payload
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocurrió un error al modificar la Batalla: {ex.Message}");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id, CancellationToken ct)
        {
            try
            {
                var existing = await _battleService.GetBattleById(id, ct);
                if (existing == null)
                    return NotFound($"No se encontró la Batalla con ID {id}");

                await _battleService.DeleteBattle(id, ct);
                return NoContent(); // 204
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocurrió un error al eliminar la Batalla: {ex.Message}");
            }
        }
    }
}