using Application.Interfaces;
using Application.Models.Dto;
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
            var ages = new List<AgeDto>();
            await foreach (var age in _ageService.GetAgeDto())
            {
                ages.Add(new AgeDto
                {
                    
                    Name = age.Name,
                    Description = age.Description,
                    Date = age.Date
                   
                });
            }
            return Ok(ages);
        }
        [HttpPost]
        public async Task<IActionResult> CreateAgeDto([FromBody] AgeDto ageDto)
        {
            if (ageDto == null)
            {
                return BadRequest("AgeDto no puede ser nulo");
            }

            var age = new Age
            {
                Name = ageDto.Name,
                Description = ageDto.Description,
                Date = ageDto.Date
            };

            var createdAge = await _ageService.CreateAgeDto(age);

            if (createdAge == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "No se pudo crear la edad.");
            }

            var createdDto = AgeDto.ToDto(createdAge);
            return CreatedAtAction(nameof(GetAllAges), createdDto);
        }
    }
}
