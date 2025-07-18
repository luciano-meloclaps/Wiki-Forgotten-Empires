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
        public IAsyncEnumerable<Age> GetAgeDto();
        public Task<Age> CreateAgeDto(Age age);
    }
}
