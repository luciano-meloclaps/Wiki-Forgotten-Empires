using Application.Interfaces;
using Application.Models.Dto;
using Application.Models.Request;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<ActionResult<IEnumerable<CharacterDtoCard>>> GetCharactersAll(CancellationToken ct)
        {
            try
            {
                var characters = await _characterService.GetAllCharacters(ct);
                return Ok(characters);
            }
            catch (Exception) 
            {
                return StatusCode(500, "Ocurrió un error al obtener los Personajes");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CharacterDtoDetail>> GetCharacter(int id, CancellationToken ct)
        {
            try
            {
                var character = await _characterService.GetCharacterById(id, ct);
                if (character == null)
                    return NotFound($"No se encontró el Personaje con id {id}");

                return Ok(character);
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocurrió un error al obtener el Personaje");
            }
        }

        [HttpPost]
        public async Task<ActionResult<CharacterDtoDetail>> CreateCharacter([FromBody] CharacterCreateRequest request, CancellationToken ct)
        {
            try
            {
                var character = await _characterService.CreateCharacter(request, ct);
                return CreatedAtAction(nameof(GetCharacter), new { id = character.Id }, character);
            }
            catch (Exception ex)
            {//Testing trucho SACALO 
                return StatusCode(500, $"Ocurrió un error al crear el Personaje: {ex.GetType().Name}{ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCharacter(int id, [FromBody] CharacterUpdateRequest request, CancellationToken ct)
        {
            try
            {
                var characterSuccess = await _characterService.UpdateCharacter(id, request, ct);
                if (!characterSuccess)
                {
                    return NotFound($"No se encontró el Personaje con id {id}");
                }
                return NoContent(); //204
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocurrió un error al crear el Personaje {ex.InnerException}{ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCharacter(int id, CancellationToken ct)
        {
            try
            {
                var characterSuccess = await _characterService.DeleteCharacter(id, ct);
                if (!characterSuccess)
                {
                    return NotFound($"No se encontró el Personaje con id {id}");
                }
                return NoContent(); //204
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocurrió un error al crear el Personaje: {ex.Message}");
            }
        }
    }
}
