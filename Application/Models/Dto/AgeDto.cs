using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Application.Models.Dto.BattleTableDto;
using static Application.Models.Dto.CivilizationGalleryDto;
using static Application.Models.Dto.CharacterDtoCard;

namespace Application.Models.Dto
{
    public class AgeAccordionDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Summary { get; set; }
        public string? Date { get; set; }

        //Entidad → DTO, salida
        public static AgeAccordionDto ToDto(Age age)
        {
            return new AgeAccordionDto
            {
                Name = age.Name,
                Summary = age.Summary,
                Date = age.Date
            };
        } 
    }
    public class AgeDetailDto
    {
        public string Name { get; set; }
        public string? Date { get; set; }
        public string? Overview { get; set; }
        public string? Summary { get; set; }

        public ICollection<CharacterDtoCard> Characters { get; set; } = new List<CharacterDtoCard>();
        public ICollection<BattleTableDto> Battles { get; set; } = new List<BattleTableDto>();
        public ICollection<CivilizationGalleryDto> Civilizations { get; set; } = new List<CivilizationGalleryDto>();


        public static AgeDetailDto ToDto(Age age)
        {
            return new AgeDetailDto
            {
                Name = age.Name,
                Summary = age.Summary,
                Date = age.Date,
                Overview = age.Overview,
                Characters = age.Characters.Select(CharacterDtoCard.ToDto).ToList(),
                Battles = age.Battles.Select(BattleTableDto.ToDto).ToList(),
                Civilizations = age.Civilizations.Select(c => CivilizationGalleryDto.ToDto(c.Civilization)).ToList()
            };
        }
    }
}
