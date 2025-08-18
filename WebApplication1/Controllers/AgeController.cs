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
                    return NotFound("No se encontraron Edades registradas.");

                return Ok(ages);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Ocurrió un error al obtener las Edades: {ex.Message}");
            }
        }

        [HttpGet("detail/{id:int}")]
        public async Task<ActionResult<AgeDetailDto>> GetByIdAsync(int id, CancellationToken ct)
        {
            try
            {
                if (id < 0)
                    return BadRequest("El ID, debe ser al menos 1");

                var result = await _ageService.GetAgeDetailByIdAsync(id, ct);

                if (result is null)
                    return NotFound($"No se encontró la edad con ID {id}.");

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Ocurrió un error al obtener el detalle de la Edad: {ex.Message}");
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
                    new { mensaje = $"Ocurrió un error al crear la Edad: {ex.Message}" });
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateAgeAsync(int id, [FromBody] UpdateAgeDto dto, CancellationToken ct)
        {
            try
            {
                var updatedAge = await _ageService.UpdateAsync(id, dto, ct);
                return Ok(updatedAge);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { mensaje = $"No se encontró la Edad con ID {id}." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { mensaje = $"Ocurrió un error al actualizar la Edad: {ex.Message}" });
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAgeAsync(int id, CancellationToken ct)
        {
            if (id < 0)
                return BadRequest(new { mensaje = "El ID debe ser al menos 1" });

            var eliminado = await _ageService.DeleteAsync(id, ct);

            if (!eliminado)
                return NotFound(new { mensaje = $"No se encontró la edad con ID {id}." });

            return NoContent();
        }


    }
}
