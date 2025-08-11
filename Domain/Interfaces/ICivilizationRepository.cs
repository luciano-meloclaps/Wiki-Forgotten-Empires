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
       Task<IEnumerable<Civilization>> GetAll();
      Task<Civilization?> GetById(int id);
     Task<Civilization> Create(Civilization civilization);
     
       Task<Civilization> Update(Civilization civilization);

        Task<bool> Delete(int id);

    }
}
