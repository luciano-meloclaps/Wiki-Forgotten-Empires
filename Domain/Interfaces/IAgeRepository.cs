using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IAgeRepository 
    {
        Task<List<Age>> GetAllAsync(CancellationToken ct = default);

        Task<Age?> GetAgeDetailByIdAsync(int id, CancellationToken ct = default);
        Task<Age> CreateAgeAsync(Age age, CancellationToken ct = default);
        Task<Age> PutDto(int id, Age age);
        Task<bool> DeleteAgeAsync(int id);
       
    }
}
