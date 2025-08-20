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
    public class BattleTableDto
    {
        public string Name { get; set; } = default!;
        // public string? Winner { get; set; } Hacer DTO de la tabla intermedia
        // public string? Loser { get; set; }
        public string? Date { get; set; }
        public TerritoryType? Territory { get; set; }
        public ICollection<string> Civilizations { get; set; } = new List<string>();
        public static BattleTableDto ToDto(Battle battle)
        {
            return new BattleTableDto
            {
                Date = battle.Date,
                Name = battle.Name,
                Civilizations = battle.Civilizations ?.Select(cb => cb.Civilization.Name).ToList() ?? new List<string>(),
                Territory = battle.Territory ?? TerritoryType.None
            };
        }
        public class BattleDetailDto
        {
            public string Name { get; set; } = default!;
            public string? Summary { get; set; }
            public string? DetailedDescription { get; set; }
            public string? Date { get; set; }
            public string? Territory { get; set; }

            public ICollection<CharacterDtoCard> Characters { get; set; } = new List<CharacterDtoCard>();
            public ICollection<CivilizationGalleryDto> Civilizations { get; set; } = new List<CivilizationGalleryDto>();

            public static BattleDetailDto ToDto(Battle battle)
            {
                return new BattleDetailDto
                {
                    Name = battle.Name,
                    Summary = battle.Summary,
                    DetailedDescription = battle.DetailedDescription,
                    Date = battle.Date,
                    Territory = battle.Territory?.ToString(),

                    Characters = battle.Characters?
                                      .Select(cb => CharacterDtoCard.ToDto(cb.Character))
                                      .ToList()
                                  ?? new List<CharacterDtoCard>(),

                    Civilizations = battle.Civilizations?
                                         .Select(cb => CivilizationGalleryDto.ToDto(cb.Civilization))
                                         .ToList()
                                     ?? new List<CivilizationGalleryDto>(),
                };
            }
        }
    }
}
