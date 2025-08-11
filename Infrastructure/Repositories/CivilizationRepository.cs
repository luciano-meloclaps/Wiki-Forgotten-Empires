using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;//Mirar usings
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class CivilizationRepository(ApplicationContext context) : ICivilizationRepository
    {
        private readonly ApplicationContext _context = context;

        public async Task<IEnumerable<Civilization>> GetAll()
        {
            return await _context.Civilizations
                .Include(c => c.Characters)
                .Include(c => c.Ages)
                .Include(c => c.Battles)
                .ToListAsync();
        }
        public async Task<Civilization?> GetById(int id)
        {
            return await _context.Civilizations
                .Include(c => c.Characters)
                .Include(c => c.Ages)
                .Include(c => c.Battles)
                .FirstOrDefaultAsync(c => c.Id == id);
        }
        public async Task<Civilization> Create(Civilization civilization)
        {
            _context.Civilizations.Add(civilization);
            await _context.SaveChangesAsync();
            return civilization;
        }

        public async Task<Civilization> Update(Civilization civilization)
        {
            _context.Civilizations.Update(civilization);
            await _context.SaveChangesAsync();
            return civilization;
        }
        public async Task<bool> Delete(int id)
        {
            var civilization = await _context.Civilizations.FindAsync(id);
            if (civilization == null) return false;
            _context.Civilizations.Remove(civilization);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
