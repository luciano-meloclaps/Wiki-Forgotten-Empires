using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ICivilizationRepository
    {
        Task<IEnumerable<Civilization>> GetAllCivilization(CancellationToken ct);
        Task<Civilization?> GetCivizlizationById(int id, CancellationToken ct);
        Task<Civilization> CreateCivilization(Civilization civilization, CancellationToken ct);
        Task UpdateCivilization(Civilization civilization, CancellationToken ct);
        Task DeleteCivilization(Civilization civilization, CancellationToken ct);
    }
}
