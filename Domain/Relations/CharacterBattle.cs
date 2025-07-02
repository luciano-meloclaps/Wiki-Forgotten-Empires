using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Relations
{ 
    public class CharacterBattle
    {
        //Tabla con Ent. Battle
        [ForeignKey("Battle")]
        public int BattleId { get; set; }
        public Battle Battle { get; set; }

        //Tabla con Ent. Character
        [ForeignKey("Character")]
        public int CharacterId { get; set; }
        public Character Character { get; set; }

        public string FactionName { get; set; }

    }
}

