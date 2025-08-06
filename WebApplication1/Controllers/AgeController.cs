using Application.Interfaces;
using Application.Models.Dto;
using Application.Models.Request.Application.Models.Request;
using Domain.Entities;
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
        public async Task<ActionResult<IEnumerable<AgeAccordionDto>>> GetAllAges()
        {
            var ages = new List<AgeAccordionDto>();
            await foreach (var age in _ageService.GetAgeDto())
            {
                ages.Add(age);
            }
            return Ok(ages);
        }


        [HttpGet("detail/{id}")]
        public async Task<ActionResult<AgeDetailDto>> GetById(int id)
        {
            var result = await _ageService.GetAgeDetailById(id);

            if (result is null)
                return NotFound();

            return Ok(result);
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateAgeDto([FromBody] CreateAgeDto ageDto)
        {
            if (ageDto == null)
            {
                return BadRequest("AgeDto no puede ser nulo");
            }
            var createdAge = await _ageService.CreateAsync(ageDto);
            return CreatedAtAction(nameof(GetById), new { id = createdAge.Id }, AgeDetailDto.ToDto(createdAge));
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
                Summary = ageDto.Description,
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
