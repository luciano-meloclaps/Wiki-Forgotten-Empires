using Application.Models.Request.Application.Models.Request;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Request
{
    public class CivilizationRequest
    {
        [Required(ErrorMessage = "El Nombre es obligatorio.")]
        public string Name { get; set; }
        [StringLength(500), Required(ErrorMessage = "El Resumen es obligatorio.")]
        public string Summary { get; set; }

        public static Civilization ToEntity(CivilizationRequest req)
        {
            return new Civilization
            {
                Name = req.Name,
                Summary = req.Summary,

            };
        }
    }
}
