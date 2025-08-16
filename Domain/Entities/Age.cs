using Domain.Relations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Age
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Summary { get; set; } //Description
        public string? Date { get; set; }
        public string? Overview { get; set; }

        //Relaciones 1->N 
        public ICollection<Character> Characters { get; set; } = new List<Character>();
        public ICollection<Battle> Battles { get; set; } = new List<Battle>();

        // / Relacion N->N con Civilization
        public ICollection<CivilizationAge> Civilizations { get; set; } = new List<CivilizationAge>();
    }
}
