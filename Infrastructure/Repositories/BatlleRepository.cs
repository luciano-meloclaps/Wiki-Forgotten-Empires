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
    public class BatlleRepository: IBattleRepository
    {
        private readonly ApplicationContext _context;
        public BatlleRepository(ApplicationContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Battle>> GetAllBattles()
        {
            return await _context.Battles
                .Include(b => b.Civilizations).ThenInclude(cb => cb.Civilization)
                .Include(b => b.Characters).ThenInclude(cb => cb.Character)
                .ToListAsync();
        }

        public async Task<Battle?> GetBattleById(int id)
        {
            return await _context.Battles
                .Include(b => b.Civilizations).ThenInclude(cb => cb.Civilization)
                .Include(b => b.Characters).ThenInclude(cb => cb.Character)
                .FirstOrDefaultAsync(b => b.Id == id);
        }
        public async Task<Battle> CreateBattle(Battle battle)
        {
            _context.Battles.Add(battle);
            await _context.SaveChangesAsync();
            return battle;
        }
        public async Task<Battle?> UpdateBattle(Battle battle)
        {
            _context.Battles.Update(battle);
            var result = await _context.SaveChangesAsync();
            return result > 0 ? battle : null;
        }
        public async Task<bool> DeleteBattleAsync(int id)
        {
            var battle = await GetBattleById(id);
            if (battle == null) return false;
            _context.Battles.Remove(battle);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        }
}
