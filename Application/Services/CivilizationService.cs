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
using Microsoft.EntityFrameworkCore;



namespace Application.Services
{
    public class CivilizationService : ICivilizationService
    {
        private readonly ICivilizationRepository _civilizationRepository;
        public CivilizationService(ICivilizationRepository civilizationRepository)
        {
            _civilizationRepository = civilizationRepository;
        }
      public async Task<IEnumerable<CivilizationGalleryDto>> GetAllCivilization()
        {
            var civilizations = await _civilizationRepository.GetAll();
            return civilizations.Select(c => new CivilizationGalleryDto
            {
                Name = c.Name,
                ImageUrl = c.ImageUrl,
                Territory = c.Territory,
                State = c.State
            });
        }
        public async Task<CivilizationDetailDto?> GetCivilizationById(int id)
        {
            var civilization = await _civilizationRepository.GetById(id); //Include method
            if (civilization == null) return null;
            return new CivilizationDetailDto
            {
                Name = civilization.Name,
                Overview = civilization.Overview,
                ImageUrl = civilization.ImageUrl,
                Territory = civilization.Territory,
                State = civilization.State,
                Characters = civilization.Characters.Select(c => new CharacterDtoCard
                {
                    Name = c.Name,
                    ImageUrl = c.ImageUrl,
                  
                }).ToList(),
                Ages = civilization.Ages.Select(ca => new AgeAccordionDto
                {
                    Date = ca.Age.Date,
                    Name = ca.Age.Name,
                    Summary = ca.Age.Summary
                }).ToList(),
                Battles = civilization.Battles.Select(cb => new BattleTableDto
                {
                    Name = cb.Battle.Name,
                    Date = cb.Battle.Date,
                  
                }).ToList()
            };
        }
        
        public async Task<CivilizationGalleryDto> CreateCivilization(CreateCivilizationRequest req)
        {
            var civilization = new Civilization
            {
                Name = req.Name,
                Territory = req.Territory,
                State = req.State
            };
            var createdCivilization = await _civilizationRepository.Create(civilization);
            return new CivilizationGalleryDto
            {
                Name = createdCivilization.Name,
                Territory = createdCivilization.Territory,
                State = createdCivilization.State
            };
        }

        public async Task<CivilizationDetailDto> UpdateCivilizationAsync(int id, UpdateCivilizationRequest req)
        {
            var civilization = await _civilizationRepository.GetById(id);
            if (civilization == null)
                throw new KeyNotFoundException("Civilization not found");

            if (req.Name != null)
                civilization.Name = req.Name;

            if (req.Overview != null)
                civilization.Overview = req.Overview;

            if (req.ImageUrl != null)
                civilization.ImageUrl = req.ImageUrl;

            if (req.Territory.HasValue)
                civilization.Territory = req.Territory.Value;

            if (req.State.HasValue)
                civilization.State = req.State.Value;

            await _civilizationRepository.Update(civilization);

            return CivilizationDetailDto.ToDto(civilization);

        }
        public async Task<bool> DeleteCivilization(int id)
        {
            try 
            {
                var civilization = await _civilizationRepository.GetById(id);
                if (civilization == null) return false;
               await _civilizationRepository.Delete(id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}