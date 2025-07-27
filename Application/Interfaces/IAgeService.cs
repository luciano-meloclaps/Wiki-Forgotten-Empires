using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAgeService
    {
         IAsyncEnumerable<Age> GetAgeDto();
         Task<Age> CreateAgeDto(Age age);
        /* Task<Age> UpdateAgeDto(int id, Age age);
         Task<Age> GetAgeByIdAsync(int id);
         Task<bool> DeleteBattleAsync(int id);*/
    }
}
