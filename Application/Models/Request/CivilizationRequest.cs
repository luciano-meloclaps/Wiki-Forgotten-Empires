using Application.Models.Request.Application.Models.Request;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Request
{
    public class CivilizationRequest
    {
        public string Name { get; set; }
        public string Summary { get; set; }
        public string ImageUrl { get; set; }

        public static Civilization ToEntity(CivilizationRequest req)
        {
            return new Civilization
            {
                Name = req.Name,
                Summary = req.Summary,
                ImageUrl = req.ImageUrl
            };
        }
    }
}
