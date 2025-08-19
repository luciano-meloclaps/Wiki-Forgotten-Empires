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
        Task<List<Age>> GetAllAsync(CancellationToken ct);
        Task<Age?> GetByIdAsync(int id, CancellationToken ct);
        Task CreateAsync(Age age, CancellationToken ct);
        Task<Age?> UpdateAsync(int id, CancellationToken ct);
        Task<(bool Eliminado, string? Nombre)> DeleteAsync(int id, CancellationToken ct);
        Task SaveChangesAsync(CancellationToken ct);
    }
}
