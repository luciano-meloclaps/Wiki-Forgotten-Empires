using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Enums;

namespace Application.Models.Dto
{
    public class BattleTableDto
    {
        public string Name { get; set; } = default!;
        public string? Date { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public TerritoryType? Territory { get; set; }

        public List<int> CharacterIds { get; set; }
        public List<int> CivilizationIds { get; set; }

        public static BattleTableDto ToDto(Battle battle)
        {
            return new BattleTableDto
            {
                Date = battle.Date,
                Name = battle.Name,
                Territory = battle.Territory,
                CharacterIds = battle?.Characters?.Select(cb => cb.CharacterId).ToList(),
                CivilizationIds = battle?.Civilizations?.Select(cb => cb.CivilizationId).ToList()
            };
        }
        
        public class BattleDetailDto
        {
            public string Name { get; set; } = default!;
            public string? Summary { get; set; }
            public string? DetailedDescription { get; set; }
            public string? Date { get; set; }
            [JsonConverter(typeof(JsonStringEnumConverter))]
            public TerritoryType? Territory { get; set; }
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
                    Territory = battle.Territory,

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
