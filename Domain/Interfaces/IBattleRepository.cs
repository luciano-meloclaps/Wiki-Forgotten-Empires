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
        Task<List<Battle>> GetAll(CancellationToken ct);
        Task<Battle?> GetByIdAsync(int id, CancellationToken ct);
        Task CreateAsync(Battle battle, CancellationToken ct);
        Task<Battle?> UpdateAsync(int id, CancellationToken ct);
        Task<(bool Eliminado, string? Nombre)> DeleteAsync(int id, CancellationToken ct);
        Task SaveChangesAsync(CancellationToken ct);
    }
}
