using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Dto
{
    public class BattleDto
    {
        public string Name { get; set; }
        public string Summary { get; set; }
        public string Date { get; set; }

        public static BattleDto ToDto(BattleDto dto)
        {
            return new BattleDto
            {
                Name = dto.Name,
                Summary = dto.Summary,
                Date = dto.Date
            };
        } }
    }
}
