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

       public async Task<IEnumerable<Battle>> GetAllAsync()
        {
            return await _context.Battles.ToListAsync();
        }
        public async Task<Battle> GetByIdAsync(int id)
        {
            return await _context.Battles.FindAsync(id);
        }
        public async Task<Battle> CreateAsync(Battle battle)
        {
            if (battle == null) throw new ArgumentNullException(nameof(battle));
            _context.Battles.Add(battle);
            await _context.SaveChangesAsync();
            return battle;
        }
        public async Task<Battle> UpdateAsync(Battle battle)
        {
            if (battle == null) throw new ArgumentNullException(nameof(battle));
            _context.Battles.Update(battle);
            await _context.SaveChangesAsync();
            return battle;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var battle = await GetByIdAsync(id);
            if (battle == null) return false;
            _context.Battles.Remove(battle);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
