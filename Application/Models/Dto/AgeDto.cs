using Domain.Entities;

namespace Application.Models.Dto
{
    public class AgeAccordionDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string? Summary { get; set; }
        public string? Date { get; set; }

        //Entidad → DTO, salida
        public static AgeAccordionDto ToDto(Age age)
        {
            return new AgeAccordionDto
            {
                Id = age.Id,
                Name = age.Name,
                Summary = age.Summary,
                Date = age.Date
            };
        }
    }

    public class AgeDetailDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string? Date { get; set; }
        public string? Overview { get; set; }
        public string? Summary { get; set; }

        //Relaciones
        public ICollection<CharacterDtoCard> Characters { get; set; } = new List<CharacterDtoCard>();

        public ICollection<BattleTableDto> Battles { get; set; } = new List<BattleTableDto>();
        public ICollection<CivilizationGalleryDto> Civilizations { get; set; } = new List<CivilizationGalleryDto>();

        public static AgeDetailDto ToDto(Age age)
        {
            return new AgeDetailDto
            {
                Id = age.Id,
                Name = age.Name,
                Summary = age.Summary,
                Date = age.Date,
                Overview = age.Overview,
                Battles = age.Battles?.Select(BattleTableDto.ToDto).ToList() ?? new List<BattleTableDto>(),
                Characters = age.Characters?.Select(CharacterDtoCard.ToDto).ToList() ?? new List<CharacterDtoCard>(),
                Civilizations = age.Civilizations?.Select(ca => CivilizationGalleryDto.ToDto(ca.Civilization)).ToList() ?? new List<CivilizationGalleryDto>()
            };
        }
    }
}