using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Enums;

namespace Application.Models.Request
{
    public class BattleRequest
    {
        public string Name { get; set; }
        public string Summary { get; set; }
        public string Date { get; set; }
       
        // DTO → Entidad, entrada
        public static Battle ToEntity(BattleRequest req)
        {
            return new Battle
            {
                Name =req.Name,
                Summary = req.Summary,
                Date = req.Date,
               

            };
        }
    }
}
