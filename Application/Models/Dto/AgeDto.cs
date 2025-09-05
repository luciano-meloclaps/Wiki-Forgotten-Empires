using System.ComponentModel.DataAnnotations;
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

        // --- CAMBIO: De un objeto a una lista de objetos ---
        public List<BattleTableDto> Battles { get; set; } = new();

        public List<CharacterDtoCard> Characters { get; set; } = new();

        public static AgeDetailDto ToDto(Age age)
        {
            return new AgeDetailDto
            {
                Id = age.Id,
                Name = age.Name,
                Summary = age.Summary,
                Date = age.Date,
                Overview = age.Overview,

                // ---  .Select() para convertir cada elemento de la colección ---
                Battles = age.Battles.Select(BattleTableDto.ToDto).ToList(),
                Characters = age.Characters.Select(CharacterDtoCard.ToDto).ToList(),
            };
        }
    }
}