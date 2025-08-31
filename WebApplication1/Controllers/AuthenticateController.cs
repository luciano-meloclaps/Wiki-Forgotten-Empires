using System.Runtime.InteropServices;
using ForgottenEmpires.API.Services;
using Microsoft.AspNetCore.Components;

namespace ForgottenEmpires.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IAuthenticateService _authenticateService;

        public AuthenticateController(IAuthenticateService authenticateService)
        {
            _authenticateService = authenticateService;
        }

        [HttpPost]
        public IActionResult Authenticate([FromBody] UserLogin userLogin)
        {
            var token = _authenticateService.Authenticate(userLogin);
            if (token == null)
            {
                return Unauthorized();
            }
            return Ok(new { Token = token });
        }
    }
}