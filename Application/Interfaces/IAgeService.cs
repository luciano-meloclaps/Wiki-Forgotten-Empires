using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAgeService
    {
        public Task<IEnumerable<Age>> GetAllAges();
        public Task<Age> CreateAge(Age age);
        public Task<Age> GetAgeById(int id);
        public Task<bool> UpdateAge(int id, Age age);
        public Task<bool> DeleteAge(int id);
    }
}
