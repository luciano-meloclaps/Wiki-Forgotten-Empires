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
    }
}
