﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums;

namespace Application.Models.Dto
{
    public class BattleDto
    {
        public string Name { get; set; }
        public string Summary { get; set; }
        public string Date { get; set; }
        public TerritoryType Territory { get; set; }
        //Entidad → DTO, salida
        public static BattleDto ToDto(Domain.Entities.Battle battle)
        {
            return new BattleDto
            {
                Name = battle.Name,
                Summary = battle.Summary,
                Date = battle.Date,
                Territory = battle.Territory
            };
        }
    }
}
