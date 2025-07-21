using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ICivilizationRepository
    {
        public Task<IEnumerable<Civilization>> GetCivilizationDto();
        public Task AddCivilization(Civilization civilization);
    }
}
