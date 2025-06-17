using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{ 
    //Resultados posibles
    public enum BattleOutcome
    {
        Victory,
        Defeat,
        Draw
    }
    public class BattleParticipation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        //Relacion con Ent. Battle
        [ForeignKey("Battle")]
        public int BattleId { get; set; }
        public Battle Battle { get; set; }

        //Relacion con Ent. Character
        [ForeignKey("Character")]
        public int CharacterId { get; set; }
        public  BattleOutcome Outcome  { get; set; }
    }
}
