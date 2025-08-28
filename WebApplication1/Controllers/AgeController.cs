using Application.Interfaces;
using Application.Models.Request;
using Microsoft.AspNetCore.Mvc;

namespace ForgottenEmpire.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgeController : ControllerBase
    {
        private readonly IAgeService _ageService;

        public AgeController(IAgeService ageService)
        {
            _ageService = ageService ?? throw new ArgumentNullException(nameof(ageService));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken ct)
        {
            try
            {
                var ages = await _ageService.GetAllAges(ct);
                return Ok(ages);
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocurrio un error al obtener las Edades.");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id, CancellationToken ct)
        {
            try
            {
                var age = await _ageService.GetAgeById(id, ct);

                if (age == null)
                {
                    return NotFound($"No se encontró la Edad con id {id}");
                }
                return Ok(age);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocurrio un error al obtener la Edad: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAgeDto dto, CancellationToken ct)
        {
            try
            {
                var ageDto = await _ageService.CreateAge(dto, ct);
                return Ok(ageDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocurrio un error al crear la Edad: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateAgeDto dto, CancellationToken ct)
        {
            try
            {
                var updatedAge = await _ageService.UpdateAge(id, dto, ct);
                if (!updatedAge)
                {
                    return NotFound($"No se encontró la Edad con id {id}");
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocurrio un error al actualizar la Edad: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken ct)
        {
            try
            {
                var deleteAge = await _ageService.DeleteAge(id, ct);
                if (!deleteAge)
                {
                    return NotFound($"No se encontró la Edad con id {id}");
                }

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, new { mensaje = "Ocurrió un error inesperado." });
            }
        }
    }
}