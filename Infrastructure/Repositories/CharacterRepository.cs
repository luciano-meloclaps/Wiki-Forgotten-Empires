using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public async Task<IEnumerable<object>> GetAllCharactersAsync()
        {
            return await _context.Characters
                .Select(c => new
                {
                    c.Name,
                    c.HonorificTitle,
                    c.ImageUrl,
                    c.LifePeriod
                })
                .ToListAsync();
        }
    }
}
