using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Models.Dto;
using Application.Models.Request;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class CharacterService : ICharacterService
    {
        private readonly ICharacterRepository _characterRepository;
        public CharacterService(ICharacterRepository characterRepository)
        {
            _characterRepository = characterRepository ?? throw new ArgumentNullException(nameof(characterRepository));
        }


        /*
       public async Task<IEnumerable<CharacterDto>> GetAllCharactersAsync()
        {
            var characters = await _characterRepository.GetAllCharactersAsync();
            if (characters == null || !characters.Any())
            {
                return Enumerable.Empty<CharacterDto>();
            }
            return characters.Select(c => new CharacterDto
            {
                Name = c.Name,
                HonorificTitle = c.HonorificTitle,
                ImageUrl = c.ImageUrl,
                LifePeriod = c.LifePeriod
            });
        }*/
        public async Task<CharacterDtoDetail> CharacterDetail(int id)
        {
            var character = await _characterRepository.GetCharacterByIdAsync(id);
            if (character == null)
            {
                return null;
            }
            return new CharacterDtoDetail
            {
                Name = character.Name,
                HonorificTitle = character.HonorificTitle,
                ImageUrl = character.ImageUrl,
                LifePeriod = character.LifePeriod,
                 Civilization = character.Civilization != null ? CivilizationDto.ToDto(character.Civilization) : null,
                  Age = character.Age != null ? AgeDto.ToDto(character.Age) : null, //Necesito insrtarlos en BD para tenes sus ids 
                 Battles = character.Battles?.Select(cb => new BattleDto
                  {
                      Name = cb.Battle.Name,
                      Summary = cb.Battle.Summary,
                  }).ToList()
            };
        }
        public async Task<IEnumerable<CharacterDtoCard>> GetAllCharactersAsync()
        {
            var characters = await _characterRepository.GetAllCharactersAsync();
            if (characters == null || !characters.Any())
            {
                return Enumerable.Empty<CharacterDtoCard>();
            }
            return characters.Select(c => CharacterDtoCard.ToDto(c));
        }
        public async Task<CharacterDtoCard> CreateCharacterAsync(CharacterCreateRequest request)
        {
            // Crear entidad (Domain)
            var character = new Character
            {
                Name = request.Name,
                // Mapear otras propiedades si las hay
            };

            // Guardar en DB (la DB generará el Id)
            await _characterRepository.AddCharacterAsync(character);

            // character.Id ya tiene valor luego de SaveChangesAsync()

            // Retornar un DTO para no exponer la entidad completa
            return new CharacterDtoCard
            {
                Name = character.Name,
            };
        }

    }
}