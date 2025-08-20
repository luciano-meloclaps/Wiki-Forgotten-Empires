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
    public class BatlleRepository : IBattleRepository
    {
        private readonly ApplicationContext _context;
        public BatlleRepository(ApplicationContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<List<Battle>> GetAll(CancellationToken ct)
        {
            return await _context.Battles
                .AsNoTracking()
                .Include(b => b.Civilizations)
                    .ThenInclude(cb => cb.Civilization)
                .OrderBy(b => b.Name)
                .ToListAsync(ct);
        }

        public async Task<Battle?> GetByIdAsync(int id, CancellationToken ct)
        {
            return await _context.Battles
                .AsNoTracking()
                .Include(b => b.Characters)
                    .ThenInclude(cb => cb.Character)
                        .ThenInclude(ch => ch.Civilization)
                .Include(b => b.Characters)
                    .ThenInclude(cb => cb.Character)
                        .ThenInclude(ch => ch.Age)
                .Include(b => b.Civilizations)
                    .ThenInclude(cb => cb.Civilization)
                .FirstOrDefaultAsync(b => b.Id == id, ct);
        }

        public async Task CreateAsync(Battle battle, CancellationToken ct)
        {
            await _context.Battles.AddAsync(battle, ct);
        }

        public async Task<Battle?> UpdateAsync(int id, CancellationToken ct)
        {
            return await _context.Battles
                .Include(b => b.Characters)
                    .ThenInclude(cb => cb.Character)
                        .ThenInclude(ch => ch.Civilization)
                .Include(b => b.Characters)
                    .ThenInclude(cb => cb.Character)
                        .ThenInclude(ch => ch.Age)
                .Include(b => b.Civilizations)
                    .ThenInclude(cb => cb.Civilization)
                .FirstOrDefaultAsync(b => b.Id == id, ct);
        }

        public async Task<(bool Eliminado, string? Nombre)> DeleteAsync(int id, CancellationToken ct)
        {
            var battle = await _context.Battles.FindAsync(new object[] { id }, ct);
            if (battle is null) return (false, null);

            var nombre = battle.Name;
            _context.Battles.Remove(battle);
            return (true, nombre);
        }

        public async Task SaveChangesAsync(CancellationToken ct)
        {
            await _context.SaveChangesAsync(ct);
        }
    }
}
