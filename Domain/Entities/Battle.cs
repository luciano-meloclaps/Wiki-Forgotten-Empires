using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Domain.Enums;
using Domain.Relations;

namespace Domain.Entities
{
    public class Battle
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Name es obligatorio.")]
        public string Name { get; set; }
        public string? DetailedDescription { get; set; }
        public string? Summary { get; set; }
        public string? Date { get; set; }

        //Enums
        public TerritoryType? Territory { get; set; }

        // Relacion N->N con Character
        public ICollection<CharacterBattle>? Characters { get; set; }

        // Relacion N->N con Civilization
        public ICollection<CivilizationBattle>? Civilizations { get; set; }

        //Relacion N->1 con Age
        public int? AgeId { get; set; }
        public Age? Age { get; set; }
    }
}
