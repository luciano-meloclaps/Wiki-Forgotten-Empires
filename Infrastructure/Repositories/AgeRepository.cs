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

        public async Task<List<Age>> GetAllAsync(CancellationToken ct)
        {
            return await _context.Ages
                .AsNoTracking()
                .OrderBy(a => a.Name)
                .ToListAsync(ct);
        }

        public async Task<Age?> GetByIdAsync(int id, CancellationToken ct)
        {
            return await _context.Ages
                .AsNoTracking()
                .Include(a => a.Characters).ThenInclude(c => c.Civilization)
                .Include(a => a.Battles)
                .Include(a => a.Civilizations).ThenInclude(ca => ca.Civilization)
                .FirstOrDefaultAsync(a => a.Id == id, ct);
        }
        public async Task CreateAsync(Age age, CancellationToken ct)
        {
            await _context.Ages.AddAsync(age, ct);
        }

        public async Task<Age?> UpdateAsync(int id, CancellationToken ct)
        {
            return await _context.Ages
                .Include(a => a.Characters).ThenInclude(c => c.Civilization)
                .Include(a => a.Battles)
                .Include(a => a.Civilizations).ThenInclude(ca => ca.Civilization)
                .FirstOrDefaultAsync(a => a.Id == id, ct); 
        }

        public async Task<(bool Eliminado, string? Nombre)> DeleteAsync(int id, CancellationToken ct)
        {
            var age = await _context.Ages.FindAsync(new object[] { id }, ct);
            if (age == null)
                return (false, null);

            var nombre = age.Name;

            _context.Ages.Remove(age);
            return (true, nombre);
        }
        public async Task SaveChangesAsync(CancellationToken ct)
        {
            await _context.SaveChangesAsync(ct);
        }
    }
}
    
