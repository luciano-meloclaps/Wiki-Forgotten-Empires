using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Amqp.Framing;
using Domain.Entities;
using Domain.Enums;
using Application.Models.Dto;

namespace Application.Models.Request
{
    public class BattleRequest
    {
        public string Name { get; set; }
        public string Summary { get; set; }
        public string Date { get; set; }
       /* public enum TerritoryType { get; set; }*/

        // Convertir de DTO a entidad
        public static Battle ToEntity(BattleRequest req)
        {
            return new Battle
            {
                Name = req.Name,
                Summary = req.Summary,
                Date = req.Date,
               /* Territory = req.Territory,*/

            };
        }
        }
    
}
