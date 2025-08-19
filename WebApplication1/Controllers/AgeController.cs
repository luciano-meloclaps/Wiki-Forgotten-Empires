using System.Linq;
using Application.Interfaces;
using Application.Models.Dto;
using Application.Models.Request;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


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
        public async Task<IActionResult> GetAll(CancellationToken ct)
        {
            try
            {
                var ages = await _ageService.GetAllAsync(ct);

                if (!ages.Any())
                    return NoContent();

                return Ok(ages);
            }
            catch (Exception)
            {
                return StatusCode(500, new
                {
                    error = "ErrorInterno",
                    mensaje = "Ocurrio un inconveniente al procesar la solicitud. Intente nuevamente en unos minutos."
                });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id, CancellationToken ct)
        {
            try
            {
                var age = await _ageService.GetByIdAsync(id, ct);

                if (age == null)
                {
                    return NotFound(new
                    {
                        error = "NoEncontrado",
                        mensaje = $"No encontramos la Edad con ID {id}."
                    });
                }

                return Ok(age);
            }
            catch (Exception)
            {
                return StatusCode(500, new
                {
                    error = "ErrorInterno",
                    mensaje = "Tuvimos un problema interno. Volve a intentar más tarde."
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAgeDto dto, CancellationToken ct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    error = "DatosInvalidos",
                    mensaje = "Revisa los datos enviados.",
                    detalles = ModelState
                });
            }

            try
            {
                var newAge = await _ageService.CreateAsync(dto, ct);
                return Ok(newAge);
            }
            catch (Exception)
            {
                return StatusCode(500, new
                {
                    error = "ErrorInterno",
                    mensaje = "Tuvimos un problema. Volve a intentar más tarde."
                });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateAgeDto dto, CancellationToken ct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    error = "DatosInvalidos",
                    mensaje = "Revisá los datos enviados.",
                    detalles = ModelState
                });
            }

            try
            {
                var updatedAge = await _ageService.UpdateAsync(id, dto, ct);
                if (updatedAge == null)
                {
                    return NotFound(new
                    {
                        error = "NoEncontrado",
                        mensaje = $"No encontramos la Edad con ID {id}."
                    });
                }

                return Ok(updatedAge);
            }
            catch (Exception)
            {
                return StatusCode(500, new
                {
                    error = "ErrorInterno",
                    mensaje = "Ocurrió un inconveniente al procesar la solicitud en el registro histórico. Por favor, intente nuevamente en unos minutos."
                });
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id, CancellationToken ct)
        {
            try
            {
                var (eliminado, nombre) = await _ageService.DeleteAsync(id, ct);

                if (!eliminado)
                    return NoContent(); // No existía → 204

                return Ok(new { mensaje = $"Edad '{nombre}' eliminada." });
            }
            catch (Exception)
            {
                return StatusCode(500, new { mensaje = "Ocurrió un error inesperado." });
            }
        }
    }
}


