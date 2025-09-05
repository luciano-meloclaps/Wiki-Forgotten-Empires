using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IAgeRepository
    {
        Task<List<Age>> GetAllAges(CancellationToken ct);

        Task<Age?> GetAgeById(int id, CancellationToken ct);

        Task<Age> CreateAge(Age age, CancellationToken ct);

        Task UpdateAge(Age ageFromRequest, CancellationToken ct);

        Task DeleteAge(Age age, CancellationToken ct);

        Task<Age?> GetTrackedAgeById(int id, CancellationToken ct);
    }
}