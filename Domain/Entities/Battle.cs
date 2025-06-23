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
    public class Battle
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Date { get; set; }

        //Enums
        public BattleOutcome Outcome { get; set; }
        public TerritoryType Territory { get; set; }


        // Relacion con la entidad CharacterBattle
        public ICollection<CharacterBattle> Characters { get; set; }

        // Relacion con la entidad CivilizationBattle
        public ICollection<CivilizationBattle> Civilizations { get; set; }
    }
}
