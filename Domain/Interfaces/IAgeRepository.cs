using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IAgeRepository 
    {
        public  Task<IEnumerable<Age>> GetAllAges();
        public Task<Age> CreateAge(Age age);
        Task<Age?> GetAgeDetailById(int id);
        Task<Age> PutDto(int id, Age age);
        Task<bool> DeleteAgeAsync(int id);
    }
}
