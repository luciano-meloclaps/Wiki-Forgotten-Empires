using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
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
        public string Description { get; set; }
        public string Date { get; set; }

        //Relación 1-N con Personajes
        public ICollection<Character> Characters { get; set; } = new List<Character>();

        //Relación N-M con Civilizaciones

    }
}
