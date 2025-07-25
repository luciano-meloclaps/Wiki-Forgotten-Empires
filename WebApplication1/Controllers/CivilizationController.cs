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
        public async Task<ActionResult<IEnumerable<CivilizationDto>>> GetAllCivilization()
        {
            var list = await _civilizationService.GetAllCivilization();
            return Ok(list);
        }


        [HttpPost]
        public async Task<IActionResult> CreateCivilization([FromBody] CivilizationRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var dto = await _civilizationService.CreateCivilization(request);
            return CreatedAtAction(nameof(GetAllCivilization),
                new { id = dto.Id }, dto);
        }

    }
}