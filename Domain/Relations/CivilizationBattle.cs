using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entities;

namespace Domain.Relations
{
    public class CivilizationBattle
    {
        //Relacion Ent. Battle
        [ForeignKey("Battle")]
        public int BattleId { get; set; }

        public Battle Battle { get; set; }

        //Relacion Ent. Civilization
        [ForeignKey("Civilization")]
        public int CivilizationId { get; set; }

        public Civilization Civilization { get; set; }
    }
}