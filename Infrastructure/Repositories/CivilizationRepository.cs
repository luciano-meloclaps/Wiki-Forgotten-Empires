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
    public class CivilizationRepository : ICivilizationRepository
    {
        private readonly ApplicationContext _context;
        public CivilizationRepository(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Civilization>> GetCivilizationDto()
        {
            return await _context.Civilizations.ToListAsync();
        }
        public async Task AddCivilization(Civilization civilization)
        {
            _context.Civilizations.Add(civilization);
            await _context.SaveChangesAsync();
        }

        }
}
