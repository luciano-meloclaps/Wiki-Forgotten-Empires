using Domain.Enums;
using Domain.Relations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Civilization
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Summary { get; set; }
        public string? Description { get; set; }
        public string ImageUrl { get; set; }

        //Enums
        public TerritoryType Territory { get; set; }
        public CivilizationState State { get; set; }

        //Relacion 1->N
        public ICollection<Character> Characters { get; set; }
            = new List<Character>();

        //Relacion N->N con Age
        public ICollection<CivilizationAge> Periods { get; set; }
            = new List<CivilizationAge>();

        //Relaciones N->N con Battle
        public ICollection<CivilizationBattle> Battles { get; set; }
            = new List<CivilizationBattle>();

    }
}
