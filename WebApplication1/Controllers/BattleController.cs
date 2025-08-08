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
        public async Task<IActionResult> GetAllBattles()
        {
            var battles = await _battleService.GetBattleTable();
            return Ok(battles);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBattleById(int id)
        {
            var battle = await _battleService.GetBattleDetail(id);
            if (battle == null) return NotFound();
            return Ok(battle);
        }
        [HttpPost]
        public async Task<IActionResult> CreateBattle([FromBody] CreateBattleDto createDto)
        {
            if (createDto == null)
                return BadRequest("Invalid battle data.");

            var battle = await _battleService.CreateBattle(createDto);       
            var resultDto = BattleDetailDto.ToDto(battle);

            return CreatedAtAction(
                nameof(GetBattleById),
                new { id = battle.Id },
                resultDto
            );
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBattle(int id, [FromBody] UpdateBattleDto updateDto)
        {
            if (updateDto == null)
                return BadRequest("Invalid battle data.");
            var updatedBattle = await _battleService.UpdateBattle(id, updateDto);
            if (updatedBattle == null) return NotFound();
            var resultDto = BattleDetailDto.ToDto(updatedBattle);
            return Ok(resultDto);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBattle(int id)
        {
            var result = await _battleService.DeleteBattle(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}