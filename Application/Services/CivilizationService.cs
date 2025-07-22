using Application.Interfaces;
using Application.Models.Dto;
using Domain.Entities;
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
               
                Name = c.Name,
                Summary = c.Summary
            });
        }

        public async Task<CivilizationDto> CreateCivilization(CivilizationDto civilizationDto)
        {
            var civilization = new Civilization
            {
                Id = civilizationDto.Id, 
                Name = civilizationDto.Name,
                Summary = civilizationDto.Summary
            };
            await _civilizationRepository.AddCivilization(civilization);
            return new CivilizationDto
            {
                Id = civilization.Id, 
                Name = civilization.Name,
                Summary = civilization.Summary
            };
        }
    }
    }
