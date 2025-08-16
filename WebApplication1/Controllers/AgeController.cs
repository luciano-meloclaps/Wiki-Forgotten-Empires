using Application.Interfaces;
using Application.Models.Dto;
using Application.Models.Request.Application.Models.Request;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Linq;


namespace ForgottenEmpire.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //Hacer Dtos
    public class AgeController : ControllerBase
    {
        private readonly IAgeService _ageService;
        public AgeController(IAgeService ageService)
        {
            _ageService = ageService ?? throw new ArgumentNullException(nameof(ageService));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AgeAccordionDto>>> GetAllAgesAsync(CancellationToken ct)
        {
            try
            {
                var ages = await _ageService.GetAllAsync(ct);

                if (!ages.Any())
                    return NotFound("No se encontraron edades registradas.");

                return Ok(ages);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Ocurrió un error al obtener las edades: {ex.Message}");
            }
        }

        [HttpGet("detail/{id:int}")]
        public async Task<ActionResult<AgeDetailDto>> GetByIdAsync(int id, CancellationToken ct)
        {
            try
            {
                if (id <= 0)
                    return BadRequest("El identificador debe ser un número positivo.");

                var result = await _ageService.GetAgeDetailByIdAsync(id, ct);

                if (result is null)
                    return NotFound($"No se encontró la edad con ID {id}.");

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Ocurrió un error al obtener el detalle de la edad: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateAgeAsync([FromBody] CreateAgeDto ageDto, CancellationToken ct)
        {
            try
            {
                var createdAge = await _ageService.CreateFromDtoAsync(ageDto, ct);

                return CreatedAtAction(
                    nameof(GetByIdAsync),
                    new { id = createdAge.Name }, 
                    createdAge
                );
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { mensaje = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { mensaje = $"Ocurrió un error al crear la edad: {ex.Message}" });
            }
        }

        [HttpPut("{id}")] 
        public async Task<IActionResult> UpdateAgeDto(int id, [FromBody] AgeDetailDto ageDto)
        {
            if (ageDto == null)
            {
                return BadRequest("AgeDto no puede ser nulo");
            }
            var age = new Age
            {
                Name = ageDto.Name,
                Summary = ageDto.Summary,
                Date = ageDto.Date,
                Overview = ageDto.Overview
            };
            try
            {
                var updatedAge = await _ageService.UpdateAgeDto(id, age);
                return Ok(AgeDetailDto.ToDto(updatedAge));
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"La entidad Age: {id} no se encuentra.");
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAgeAsync(int id)
        {
            var age = await _ageService.GetAgeDetailById(id);
            if (age == null)
            {
                return NotFound($"La entidad Age: {id} no se encuentra.");
            }
            var deleted = await _ageService.DeleteAgeAsync(id);
            if (!deleted)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "No se pudo eliminar la edad.");
            }
            return NoContent();
        }

    }
}
