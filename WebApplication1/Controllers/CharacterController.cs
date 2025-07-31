using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application.Services;
using Application.Interfaces;
namespace ForgottenEmpire.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterController : ControllerBase
    {
        private readonly ICharacterService _characterService;
        public CharacterController(ICharacterService characterService)
        {
            _characterService = characterService ?? throw new ArgumentNullException(nameof(characterService));
        }
        [HttpGet]
        public IActionResult GetAllCharacter(int id)
        {
            try
            {
                var characters = _characterService.GetAllCharactersAsync().Result;
                if (characters == null || !characters.Any())
                {
                    return NotFound("No characters found.");
                }
                return Ok(characters);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }


        }
    }
}
