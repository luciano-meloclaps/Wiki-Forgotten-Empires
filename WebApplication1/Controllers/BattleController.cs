using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ForgottenEmpire.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BattleController : ControllerBase
    {
        [HttpGet("{id}")]
        public IActionResult GetBattleById(int id)
        {
            // Placeholder for actual implementation
            return Ok($"Battle with ID: {id}");
        }
    }
}
