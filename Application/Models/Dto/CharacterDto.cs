using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Enums;

namespace Application.Models.Dto
{
    public class CharacterDtoCard
    {
        public string Name { get; set; }
        public string? HonorificTitle { get; set; }
        public string? ImageUrl { get; set; }
        public string? LifePeriod { get; set; }

        public static CharacterDtoCard ToDto(Character character)
        {
            return new CharacterDtoCard
            {
                Name = character.Name,
                HonorificTitle = character.HonorificTitle,
                ImageUrl = character.ImageUrl,
                LifePeriod = character.LifePeriod,
            };
        }
        
    }
    public class CharacterDtoDetail
    {
        public string? Name { get; set; }
        public string? HonorificTitle { get; set; }
        public string? ImageUrl { get; set; }
        public string? LifePeriod { get; set; }
        public string? Dynasty { get; set; }

        // Relacion N<-1 con Civilization
        public CivilizationGalleryDto? Civilization { get; set; }
       public AgeAccordionDto? Age { get; set; }
        //Enums 
        public RoleCharacter? Role { get; set; }
        //Relacion N-N con Battle
        public ICollection<BattleTableDto>? Battles { get; set; } = new List<BattleTableDto>();

        // Relacion N->N con Battle
        /* public ICollection<CharacterBattleDto>? Battles { get; set; } = new List<CharacterBattleDto>();*/

        public static CharacterDtoDetail ToDto(Character character)
        {
            return new CharacterDtoDetail //Ver relacion
            {
                Name = character.Name,
                HonorificTitle = character.HonorificTitle,
                ImageUrl = character.ImageUrl,
                LifePeriod = character.LifePeriod,
                Dynasty = character.Dynasty,
                Civilization = character.Civilization != null ? CivilizationGalleryDto.ToDto(character.Civilization) : null,
                Age = character.Age != null ? AgeAccordionDto.ToDto(character.Age) : null,
                Battles = character.Battles?.Select(cb => new BattleTableDto
                {
                    Name = cb.Battle.Name,
                    Date = cb.Battle.Date,
                }).ToList(),
            };
        }

        public static object ToEntity(CharacterDtoDetail characterDto)
        {
            throw new NotImplementedException();
        }
    }
}


