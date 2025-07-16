using Application.Interfaces;
using Application.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> GetAllAges()
        {
            var ages = await _ageService.GetAllAges();
            return Ok(ages);
        }
        [HttpPost]
        public async Task<IActionResult> CreateAge([FromBody] Age age)
        {
            if (age == null)
            {
                return BadRequest("Age cannot be null");
            }
            var createdAge = await _ageService.CreateAge(age);
            return CreatedAtAction(nameof(GetAgeById), new { id = createdAge.Id }, createdAge);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAgeById(int id)
        {
            var age = await _ageService.GetAgeById(id);
            if (age == null)
            {
                return NotFound();
            }
            return Ok(age);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAge(int id, [FromBody] Age age)
        {
            if (age == null)
            {
                return BadRequest("Age cannot be null");
            }
            var result = await _ageService.UpdateAge(id, age);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
