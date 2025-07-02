using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Relations
{
    public class CivilizationAge
    {
        //Tabla con Ent. Age
        [ForeignKey("Age")]
        public int AgeId { get; set; }
        public Age Age { get; set; }

        //Tabla con Ent. Civilization
        [ForeignKey("Civilization")]
        public int CivilizationId { get; set; }
        public Civilization Civilization { get; set; }
    }
}
