using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Models.Dto;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class CharacterService: ICharacterService
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
                Age = character.Age != null ? AgeDto.ToDto(character.Age) : null,
              /*  Battles = character.Battles?.Select(cb => new BattleDto
                {
                    Name = cb.Battle.Name,
                    Summary = cb.Battle.Summary,
                    Year = cb.Battle.Year,
                }).ToList()*/
            };
        }
    }
}
