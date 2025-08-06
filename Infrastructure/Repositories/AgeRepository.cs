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
        public async Task<IEnumerable<Age>> GetAllAges()
        {
            return await _context.Ages.ToListAsync();
        }
        public async Task<Age> CreateAge(Age age)
        {
            _context.Ages.Add(age);
            await _context.SaveChangesAsync();
            return age;
        }

        public async Task<Age?> GetAgeDetailById(int id)
        {
            return await _context.Ages
                //Civilization del PERSONAJE Include, asi se parecen a las CARDS
                .Include(a => a.Characters).ThenInclude(c => c.Civilization)
                .Include(a => a.Battles)
                .Include(a => a.Civilizations).ThenInclude(ac => ac.Civilization)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Age> PutDto(int id, Age age)
        {
            var existingAge = await GetAgeDetailById(id);
            if (existingAge == null) throw new KeyNotFoundException($"No se encuentra a la entidad Age: {id}.");
            existingAge.Name = age.Name;
            existingAge.Date = age.Date;
            existingAge.Summary = age.Summary;
            _context.Ages.Update(existingAge);
            await _context.SaveChangesAsync();
            return existingAge;
        }
        public async Task<bool> DeleteAgeAsync(int id)
        {
            var age = await GetAgeDetailById(id);
            if (age == null) return false;
            _context.Ages.Remove(age);
            await _context.SaveChangesAsync();
            return true; 
        }

    }
}
