using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Models.Dto;
using Application.Models.Request;

namespace Application.Interfaces
{
    public interface IBattleService
    {
      Task<IEnumerable<BattleDto>> GetAllBattlesAsync();
        Task<BattleDto> GetBattleByIdAsync(int id);
        Task<BattleDto> CreateBattleAsync(BattleDto battle);
        Task<BattleDto> UpdateBattleAsync(int id, BattleRequest request);
        Task<bool> DeleteBattleAsync(int id);
    }
}
