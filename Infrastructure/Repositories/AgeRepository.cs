using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class AgeRepository : IAgeRepository
    {
        private readonly ApplicationContext _context;

        public AgeRepository(ApplicationContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<List<Age>> GetAllAges(CancellationToken ct)
        {
            return await _context.Ages
           .AsNoTracking()
           .ToListAsync(ct);
        }

        public async Task<Age?> GetAgeById(int id, CancellationToken ct)
        {
            return await _context.Ages

                .Include(a => a.Battles)
                .Include(a => a.Characters)
                 .Include(a => a.Civilizations)
            .ThenInclude(ca => ca.Civilization)
        .FirstOrDefaultAsync(a => a.Id == id, ct);
        }

        public async Task<Age> CreateAge(Age age, CancellationToken ct)
        {
            await _context.Ages.AddAsync(age, ct);
            await _context.SaveChangesAsync(ct);
            return age;
        }

        public async Task UpdateAge(Age age, CancellationToken ct)
        {
            await _context.SaveChangesAsync(ct);
        }

        public async Task DeleteAge(Age age, CancellationToken ct)
        {
            _context.Ages.Remove(age);
            await _context.SaveChangesAsync(ct);
        }

        public async Task<Age?> GetTrackedAgeById(int id, CancellationToken ct)
        {
            return await _context.Ages.FirstOrDefaultAsync(a => a.Id == id, ct);
        }
    }
}