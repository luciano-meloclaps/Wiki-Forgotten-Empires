using Application.Interfaces;
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

    }
}
