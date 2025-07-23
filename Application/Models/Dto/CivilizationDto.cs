using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Dto
{
    public class CivilizationDto
    { 
        public string Name { get; set; }
        public string Summary { get; set; }
        public string ImageUrl { get; set; }
        public string State { get; set; }
        //Enums
        public string Territory { get; set; }
        public int Id { get; internal set; }
        public string Description { get; set; }
        //Entidad → DTO, salida
        public static CivilizationDto ToDto(Domain.Entities.Civilization civilization)
        {
            return new CivilizationDto
            {
                Name = civilization.Name,
                Summary = civilization.Summary,
                ImageUrl = civilization.ImageUrl,
                State = civilization.State.ToString(),
                Territory = civilization.Territory.ToString(),
            };
        }
    }
}
