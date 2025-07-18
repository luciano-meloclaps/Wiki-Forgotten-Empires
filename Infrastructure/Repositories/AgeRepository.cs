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
    }
}
