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
    public class CivilizationGalleryDto
    {
        [Required]
        public string Name { get; set; }
        public string? ImageUrl { get; set; }
        public string? Summary { get; set; } //No se si se va a terminar utiliznaodo
        public static CivilizationGalleryDto ToDto(Civilization civilization)
        {
            return new CivilizationGalleryDto
            {
                Name = civilization.Name,
                ImageUrl = civilization.ImageUrl,
                Summary = civilization.Summary,
            };
        }

    }
    public class CivilizationDetailDto
    {
        [Required]
        public string Name { get; set; }
        public string? Overview { get; set; } //Agregar en BD
        public string? ImageUrl { get; set; }
        //Enums
        public TerritoryType Territory { get; set; }
        public CivilizationState State { get; set; }
        //Relacion 1->N
        public ICollection<CharacterDtoCard>? Characters { get; set; } = new List<CharacterDtoCard>();
        //Relacion N->N con Age
        public ICollection<AgeAccordionDto>? Periods { get; set; } = new List<AgeAccordionDto>();
        //Relaciones N->N con Battle
        public ICollection<BattleTableDto>? Battles { get; set; } = new List<BattleTableDto>();
        public static CivilizationDetailDto ToDto(Civilization civilization)
        {
            return new CivilizationDetailDto
            {
                Name = civilization.Name,
                Overview = civilization.Overview,
                ImageUrl = civilization.ImageUrl,
                Territory = civilization.Territory,
                State = civilization.State,
                Characters = civilization.Characters.Select(c => CharacterDtoCard.ToDto(c)).ToList(),
                Periods = civilization.Periods.Select(p => AgeAccordionDto.ToDto(p.Age)).ToList(),
                Battles = civilization.Battles.Select(b => BattleTableDto.ToDto(b.Battle)).ToList()
            };
        }
    }
}
