 using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Request
{
    namespace Application.Models.Request
    {
        public class CreateAgeDto
        {
            [Required(ErrorMessage = "El campo Name es obligatorio.")]
            public string Name { get; set; }
            [StringLength(150, ErrorMessage = "El campo Summary no debe superar los 150 caracteres.")]
            public string? Summary { get; set; }
            public string? Date { get; set; }
            // public string? Overview { get; set; } No deberia para sobrecargar al usuario

            public static Age ToEntity(CreateAgeDto req)
            {
                return new Age
                {
                    Name = req.Name,
                    Date = req.Date,
                    Summary = req.Summary
                };
            }
        }
        public class UpdateAgeDto
        {
            public string? Name { get; set; }
            public string? Summary { get; set; }
            public string? Date { get; set; }
            public string? Overview { get; set; }
            public static Age ToEntity(UpdateAgeDto req)
            {
                return new Age
                {
                    Name = req.Name,
                    Summary = req.Summary,
                    Date = req.Date,
                    Overview = req.Overview
                };
            }
        }
    }
}