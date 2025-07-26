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

       /* public Task<IEnumerable<Battle>> GetAllBattlesAsync()
        {
            throw new NotImplementedException();
        }*/
    }
}
