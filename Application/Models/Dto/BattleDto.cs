using System.Text.Json.Serialization;
using Domain.Entities;
using Domain.Enums;

namespace Application.Models.Dto
{
    public class BattleTableDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string? Date { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public TerritoryType? Territory { get; set; }

        public static BattleTableDto ToDto(Battle battle)
        {
            return new BattleTableDto
            {
                Id = battle.Id,
                Date = battle.Date,
                Name = battle.Name,
                Territory = battle.Territory,
            };
        }

        public class BattleDetailDto
        {
            public int Id { get; set; }
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
                    Id = battle.Id,
                    Name = battle.Name,
                    Summary = battle.Summary,
                    DetailedDescription = battle.DetailedDescription,
                    Date = battle.Date,
                    Territory = battle.Territory,
                    //Characters = battle.Characters.Select(cb => CharacterDtoCard.ToDto(cb.Character)).ToList(),
                    //Civilizations = battle.Civilizations.Select(cb => CivilizationGalleryDto.ToDto(cb.Civilization)).ToList()
                };
            }
        }
    }
}