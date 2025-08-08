using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Models.Dto;
using Application.Models.Request;
using Domain.Entities;
using static Application.Models.Dto.BattleTableDto;
using static Application.Models.Request.CreateBattleDto;

namespace Application.Interfaces
{
    public interface IBattleService
    {
        Task<IEnumerable<BattleTableDto>> GetBattleTable();
        Task<BattleDetailDto?> GetBattleDetail(int id);
        Task<Battle> CreateBattle(CreateBattleDto dto);
        Task<Battle?> UpdateBattle(int id, UpdateBattleDto dto);
        Task<bool> DeleteBattle(int id);
    }

}

