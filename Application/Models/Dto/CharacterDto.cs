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
        [Required]
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
        public string? Description { get; set; }
        public string? HonorificTitle { get; set; }
        public string? ImageUrl { get; set; }
        public string? LifePeriod { get; set; }
        public string? Dynasty { get; set; }

        // Relacion N<-1 con Civilization
        public CivilizationDto? Civilization { get; set; }
       public AgeDto? Age { get; set; }
        //Enums 
        public RoleCharacter? Role { get; set; }
        //Relacion N-N con Battle
        public ICollection<BattleDto>? Battles { get; set; } = new List<BattleDto>();

        // Relacion N->N con Battle
        /* public ICollection<CharacterBattleDto>? Battles { get; set; } = new List<CharacterBattleDto>();*/

        public static CharacterDtoDetail ToDto(Character character)
        {
            return new CharacterDtoDetail
            {
                Name = character.Name,
                Description = character.Description,
                HonorificTitle = character.HonorificTitle,
                ImageUrl = character.ImageUrl,
                LifePeriod = character.LifePeriod,
                Dynasty = character.Dynasty,
                Civilization = character.Civilization != null ? CivilizationDto.ToDto(character.Civilization) : null,
               Age = character.Age != null ? AgeDto.ToDto(character.Age) : null,
                Battles = character.Battles?.Select(cb => new BattleDto
                {
                    Name = cb.Battle.Name,
                    Summary = cb.Battle.Summary,
                }).ToList(),
            };
        }

        public static object ToEntity(CharacterDtoDetail characterDto)
        {
            throw new NotImplementedException();
        }
    }
}


