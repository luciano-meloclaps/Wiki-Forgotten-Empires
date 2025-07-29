using Application.Interfaces;
using Application.Models.Dto;
using Application.Models.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
            var battles = await _battleService.GetAllBattlesAsync();
            return Ok(battles);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBattleById(int id)
        {
            var battle = await _battleService.GetBattleByIdAsync(id);
            if (battle == null) return NotFound();
            return Ok(battle);
        }
        [HttpPost]
        public async Task<IActionResult> CreateBattle([FromBody] BattleDto battleDto)
        {
            if (battleDto == null) return BadRequest("Invalid battle data.");
            var createdBattle = await _battleService.CreateBattleAsync(battleDto);
            return CreatedAtAction(nameof(GetBattleById), createdBattle);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBattle(int id, [FromBody] BattleRequest request)
        {
            if (request == null) return BadRequest("Invalid battle data.");
            var updatedBattle = await _battleService.UpdateBattleAsync(id, request);
            if (updatedBattle == null) return NotFound();
            return Ok(updatedBattle);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBattle(int id)
        {
            var result = await _battleService.DeleteBattleAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}