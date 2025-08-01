using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace Infrastructure.Repositories
{
    public class CharacterRepository : ICharacterRepository
    {
        private readonly ApplicationContext _context;
        public CharacterRepository(ApplicationContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<IEnumerable<Character>> GetAllCharactersAsync()
        {
            return await _context.Characters
                .Include(c => c.Civilization)
                .Include(c => c.Age)
                .Include(c => c.Battles)
                .ThenInclude(cb => cb.Battle)
                .ToListAsync();
        }   
        public async Task<Character> GetCharacterByIdAsync(int id)
        {
            return await _context.Characters
                .Include(c => c.Civilization)
                .Include(c => c.Age)
                .Include(c => c.Battles)
                .ThenInclude(cb => cb.Battle)
                .FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
