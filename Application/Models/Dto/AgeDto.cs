using Application.Models.Request.Application.Models.Request;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Dto
{
    public class AgeDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Date { get; set; }

        //Entidad → DTO, salida
        public static AgeDto ToDto(Age age)
        {
            return new AgeDto
            {
                Name = age.Name,
                Description = age.Description,
                Date = age.Date
            };
        }

        //Request → Entidad, entrada
        public static Age ToEntity(AgeRequest req)
        {
            return new Age
            {
                Name = req.Name,
                Description = req.Description,
                Date = req.Date
            };
        }
    }
}
