using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Character
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Civilization { get; set; }
        public string Role { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string Age { get; set; }

        // Proximamente sera añadido un campo para la fecha de nacimiento y muerte de los personajes
        //De esa manera se podra calcular la edad de los personajes
        public string LifePeriod { get; set; }

        // Propiedad de navegación para las participaciones en batallas
        public virtual ICollection<BattleParticipation> Participations { get; set; }

    }
}
