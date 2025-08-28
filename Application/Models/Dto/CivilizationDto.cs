﻿using System.Text.Json.Serialization;
using Domain.Entities;
using Domain.Enums;

namespace Application.Models.Dto
{
    public class CivilizationGalleryDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = default!;

        public string? ImageUrl { get; set; }

        // public string? Summary { get; set; } //No se si se va a terminar utiliznaodo
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public TerritoryType Territory { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public CivilizationState State { get; set; }

        public static CivilizationGalleryDto ToDto(Civilization civilization)
        {
            return new CivilizationGalleryDto
            {
                Id = civilization.Id,
                Name = civilization.Name,
                ImageUrl = civilization.ImageUrl,
                //Summary = civilization.Summary,
                Territory = civilization.Territory,
                State = civilization.State
            };
        }
    }

    public class CivilizationDetailDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string? Overview { get; set; } //Agregar en BD
        public string? ImageUrl { get; set; }

        //Enums
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public TerritoryType? Territory { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public CivilizationState? State { get; set; }

        //Relacion 1->N
        public ICollection<CharacterDtoCard>? Characters { get; set; } = new List<CharacterDtoCard>();

        //Relacion N->N con Age
        public ICollection<AgeAccordionDto>? Ages { get; set; } = new List<AgeAccordionDto>();

        //Relaciones N->N con Battle
        public ICollection<BattleTableDto>? Battles { get; set; } = new List<BattleTableDto>();

        public static CivilizationDetailDto ToDto(Civilization civilization)
        {
            return new CivilizationDetailDto
            {
                Id = civilization.Id,
                Name = civilization.Name,
                Overview = civilization.Overview,
                ImageUrl = civilization.ImageUrl,
                Territory = civilization.Territory,
                State = civilization.State,
                Characters = civilization.Characters.Select(c => CharacterDtoCard.ToDto(c)).ToList(),
                Ages = civilization.Ages.Select(a => AgeAccordionDto.ToDto(a.Age)).ToList(),
                Battles = civilization.Battles.Select(b => BattleTableDto.ToDto(b.Battle)).ToList()
            };
        }
    }
}