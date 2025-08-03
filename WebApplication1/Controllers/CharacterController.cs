using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application.Services;
using Application.Interfaces;
using Application.Models.Dto;
using Application.Models.Request;
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
        /* [HttpGet]
         public IActionResult GetAllCharacter(int id)
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
         }*/
        [HttpGet("{id}")]
        public IActionResult GetCharacter(int id)
        {
            try
            {
                var character = _characterService.CharacterDetail(id).Result;
                if (character == null)
                {
                    return NotFound($"Personaje con {id} no se encontro");
                }
                return Ok(character);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("all")]
        public IActionResult GetAllCharacters()
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
        [HttpPost]
        public async Task<IActionResult> CreateCharacter([FromBody] CharacterCreateRequest request)
        {
            if (request == null)
                return BadRequest("Invalid request.");

           var createdCharacter = await _characterService.CreateCharacterAsync(request);

            return Ok($"El personaje '{createdCharacter.Name}' fue creado con éxito.");
        }





    }
}