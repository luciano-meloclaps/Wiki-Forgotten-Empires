using Application.Interfaces;
using Application.Models.Dto;
using Application.Models.Request;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CivilizationService : ICivilizationService
    {
        private readonly ICivilizationRepository _civilizationRepository;
        public CivilizationService(ICivilizationRepository civilizationRepository)
        {
            _civilizationRepository = civilizationRepository;
        }
        public async Task<IEnumerable<CivilizationDto>> GetAllCivilization()
        {
            var civilizations = await _civilizationRepository.GetCivilizationDto();
            return civilizations.Select(c => new CivilizationDto
            {

                Id = c.Id,
                Name = c.Name,
                Summary = c.Summary,
                ImageUrl = c.ImageUrl,
                State= c.State.ToString(),
                Territory = c.Territory.ToString()
            });
        }
        public async Task<CivilizationDto> CreateCivilization(CivilizationRequest req)
        {      
            var civ = CivilizationRequest.ToEntity(req);

            civ.Description = !string.IsNullOrWhiteSpace(req.Summary)
                ? (req.Summary.Length <= 100
                      ? req.Summary
                      : req.Summary.Substring(0, 100) + "…")
                : string.Empty;
            civ.Description = "None";
            civ.Territory = TerritoryType.None;
            civ.State = CivilizationState.None;
            civ.ImageUrl = "https://example.com/default-image.png"; 

            await _civilizationRepository.AddCivilization(civ);

            var dto = CivilizationDto.ToDto(civ);
            dto.Id = civ.Id;       
            return dto;
        }
    }
}