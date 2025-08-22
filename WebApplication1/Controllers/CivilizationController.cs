using Application.Interfaces;
using Application.Models.Dto;
using Application.Models.Request;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ForgottenEmpire.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CivilizationController : ControllerBase
    {
        private readonly ICivilizationService _civilizationService;
        public CivilizationController(ICivilizationService civilizationService)
        {
            _civilizationService = civilizationService ?? throw new ArgumentNullException(nameof(civilizationService));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CivilizationGalleryDto>>> GetCivilizationsAll(CancellationToken ct)
        {
            try
            {
                var civilizations = await _civilizationService.GetAllCivilization(ct);
                return Ok(civilizations);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocurrio un error al obtener las Civilizaciones: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CivilizationDetailDto>> GetCivilizationById(int id, CancellationToken ct)
        {
            try
            {
                await _civilizationService.GetCivizlizationById(id, ct);
                var civilization = await _civilizationService.GetCivizlizationById(id, ct);
                return Ok(civilization);
            }
            
            catch (KeyNotFoundException)
            {
                return NotFound($"No se encontró la Civilización con id {id}.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocurrio un error al obtener las Civilizaciones: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<CivilizationDetailDto>> CreateCivilization(CreateCivilizationRequest request, CancellationToken ct)
        {
            try
            {
                var civilization = await _civilizationService.CreateCivilization(request, ct);
                return Ok(civilization);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocurrio un error al obtener las Civilizaciones: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCivilization(int id, UpdateCivilizationRequest request, CancellationToken ct)
        {
            try
            {
                await _civilizationService.UpdateCivilization(id, request, ct);
                return Ok();
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"No se encontró la Civilizacion con id {id}.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocurrio un error al obtener las Civilizaciones: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCivilization(int id, CancellationToken ct)
        {
            try
            {
                await _civilizationService.DeleteCivilization(id, ct);
                return Ok();
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"No se encontró la Civilización con id {id}.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocurrio un error al obtener las Civilizaciones: {ex.Message}");
            }
        }

    }
}