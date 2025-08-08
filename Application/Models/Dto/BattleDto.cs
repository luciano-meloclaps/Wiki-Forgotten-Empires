using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Enums;

namespace Application.Models.Dto
{
    public class BattleTableDto
    {
        public int Id { get; set; }
        public string? Date { get; set; }
        public string Name { get; set; } = default!;
        // public string? Winner { get; set; } Hacer DTO de la tabla intermedia
        // public string? Loser { get; set; }
        public TerritoryType? Territory { get; set; }
        public ICollection<string> Civilizations { get; set; } = new List<string>();
        public static BattleTableDto ToDto(Battle battle)
        {
            return new BattleTableDto
            {
                Id = battle.Id,
                Date = battle.Date,
                Name = battle.Name,
                Civilizations = battle.Civilizations ?.Select(cb => cb.Civilization.Name).ToList() ?? new List<string>(),
                /*Winner = battle.Characters.FirstOrDefault(cb => cb.Outcome == ParticipantOutcome.Victory)?.Character.Name,
                Loser = battle.Characters.Where(cb => cb.Outcome == ParticipantOutcome.Defeat).Select(cb => cb.Character.Name).FirstOrDefault(),*/
                Territory = battle.Territory ?? TerritoryType.None
            };
        }
        public class BattleDetailDto
        {
            public int Id { get; set; }
            public string Name { get; set; } = default!;
            public string? Summary { get; set; }
            public string? DetailedDescription { get; set; }
            public string? Date { get; set; }
            public string? Territory { get; set; }
            // public string? Winner { get; set; }
            // public string? Loser { get; set; }

            // Relaciones a mostrar con DTOs ya existentes
            public ICollection<CharacterDtoCard> Characters { get; set; } = new List<CharacterDtoCard>();
            public ICollection<CivilizationDto> Civilizations { get; set; } = new List<CivilizationDto>();

            public static BattleDetailDto ToDto(Battle battle)
            {
                return new BattleDetailDto
                {
                    Id = battle.Id,
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
                                         .Select(cb => CivilizationDto.ToDto(cb.Civilization))
                                         .ToList()
                                     ?? new List<CivilizationDto>(),
                   /*Winner = battle.Characters.FirstOrDefault(cb => cb.Outcome == ParticipantOutcome.Victory)?.Character.Name
                    Loser = battle.Characters.Where(cb => cb.Outcome == ParticipantOutcome.Defeat).Select(cb => cb.Character.Name).FirstOrDefault()*/ //Tengo que hacer un DTOde la tabla intermedia para traer los datos
                };
            }
        }
    }
}
