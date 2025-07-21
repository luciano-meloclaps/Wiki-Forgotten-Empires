using Application.Interfaces;
using Application.Models.Dto;
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
        public IActionResult GetAllCivilization() {
            var civilizations = _civilizationService.GetAllCivilization();
            
            return Ok(civilizations);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCivilization([FromBody] CivilizationDto civilizationDto)
        {
            if (civilizationDto == null)
            {
                return BadRequest("Civilization data is null.");
            }
            var createdCivilization = await _civilizationService.CreateCivilization(civilizationDto);
            return CreatedAtAction(nameof(CreateCivilization), new { id = createdCivilization.Id }, createdCivilization);
        }
    }
}