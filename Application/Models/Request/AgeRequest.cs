using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Request
{
    namespace Application.Models.Request
    {
        public class AgeRequest
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public string Date { get; set; }

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
}