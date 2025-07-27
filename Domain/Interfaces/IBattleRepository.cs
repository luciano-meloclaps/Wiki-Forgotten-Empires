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
        Task<IEnumerable<Battle>> GetAllAsync();
        Task<Battle> GetByIdAsync(int id);
        Task<Battle> CreateAsync(Battle battle);
        Task<Battle> UpdateAsync(Battle battle);
        Task<bool> DeleteAsync(int id);
    }
}
