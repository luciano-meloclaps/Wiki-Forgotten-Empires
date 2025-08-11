using Application.Interfaces;
using Application.Models.Dto;
using Application.Models.Request;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
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
        public async Task<ActionResult<IEnumerable<CivilizationGalleryDto>>> GetAllCivilizations()
        {
            var civilizations = await _civilizationService.GetAllCivilization();
            return Ok(civilizations);
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<CivilizationDetailDto?>> GetCivilizationById(int id)
        {
            var civilization = await _civilizationService.GetCivilizationById(id);
            if (civilization == null)
            {
                return NotFound();
            }
            return Ok(civilization);
        }
        [HttpPost]
        public async Task<ActionResult<CivilizationDetailDto>> CreateCivilizationAsync([FromBody] CreateCivilizationRequest req)
        {
            if (req == null)
            {
                return BadRequest("Invalid request data.");
            }
            var createdCivilization = await _civilizationService.CreateCivilization(req);
            return Ok($"La civilización '{createdCivilization.Name}' fue creado con éxito.");
        }
       

        [HttpPut("{id:int}")]
        public async Task<ActionResult<CivilizationDetailDto>> UpdateCivilizationAsync(int id, [FromBody] UpdateCivilizationRequest req)
        {
            if (req == null)
            {
                return BadRequest("Invalid request data.");
            }
            var updatedCivilization = await _civilizationService.UpdateCivilizationAsync(id, req);
            if (updatedCivilization == null)
            {
                return NotFound();
            }
            return Ok(updatedCivilization);
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<bool>> DeleteCivilization(int id)
        {
            var result = await _civilizationService.DeleteCivilization(id);
            if (!result)
            {
                return NotFound();
            }
            return Ok(result);
        }

    }
}