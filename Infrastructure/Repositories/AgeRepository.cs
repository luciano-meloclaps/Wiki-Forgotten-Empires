using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class AgeRepository : IAgeRepository
    {
        private readonly ApplicationContext _context;
        public AgeRepository(ApplicationContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<List<Age>> GetAllAsync(CancellationToken ct = default)
        {
            return await _context.Ages
                .AsNoTracking()
                .ToListAsync(ct);
        }
        public async Task<Age?> GetAgeDetailByIdAsync(int id, CancellationToken ct = default)
        {
            return await _context.Ages
                .AsNoTracking()
                .Include(a => a.Characters).ThenInclude(c => c.Civilization)
                .Include(a => a.Battles)
                .Include(a => a.Civilizations).ThenInclude(ac => ac.Civilization)
                .FirstOrDefaultAsync(a => a.Id == id, ct);
        }

        public async Task<Age> CreateAgeAsync(Age age, CancellationToken ct = default)
        {
            await _context.Ages.AddAsync(age, ct);
            await _context.SaveChangesAsync(ct);
            return age;
        }


        public async Task<Age> UpdateAsync(int id, Age age, CancellationToken ct = default)
        {
            var existingAge = await _context.Ages
                .FirstOrDefaultAsync(a => a.Id == id, ct);

            if (existingAge is null)
                throw new KeyNotFoundException($"No se encontró la Edad con ID {id}.");

            existingAge.Name = age.Name ?? existingAge.Name;
            existingAge.Summary = age.Summary ?? existingAge.Summary;
            existingAge.Date = age.Date ?? existingAge.Date;
            existingAge.Overview = age.Overview ?? existingAge.Overview;

            _context.Ages.Update(existingAge);
            await _context.SaveChangesAsync(ct);

            return existingAge;
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken ct = default)
        {
            var entity = await _context.Ages
                .FirstOrDefaultAsync(a => a.Id == id, ct);

            if (entity is null)
                return false;

            _context.Ages.Remove(entity);
            await _context.SaveChangesAsync(ct);
            return true;
        }


    }
}
