using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;


namespace Domain.Interfaces
{
    public interface IBattleRepository
    {
       Task<IEnumerable<Battle>> GetAllBattles();
       Task<Battle?> GetBattleById(int id);
         Task<Battle> CreateBattle(Battle battle);
            Task<Battle?> UpdateBattle(Battle battle);
            Task<bool> DeleteBattleAsync(int id);

    }
}
