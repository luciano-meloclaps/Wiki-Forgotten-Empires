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
            if (age == null) throw new ArgumentNullException(nameof(age));
            _context.Ages.Add(age);
            await _context.SaveChangesAsync();
            return age;
        }
        public async Task<Age> GetAgeById(int id)
        {
            return await _context.Ages.FindAsync(id);
        }
        public async Task<bool> UpdateAge(int id, Age age)
        {
            if (age == null) throw new ArgumentNullException(nameof(age));
            var existingAge = await _context.Ages.FindAsync(id);
            if (existingAge == null) return false;
            existingAge.Name = age.Name;
            existingAge.Description = age.Description;
            existingAge.Date = age.Date;
            _context.Ages.Update(existingAge);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteAge(int id)
        {
            var age = await _context.Ages.FindAsync(id);
            if (age == null) return false;
            _context.Ages.Remove(age);
            await _context.SaveChangesAsync();
            return true;

        }
    }
        
}
