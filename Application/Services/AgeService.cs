using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class AgeService : IAgeService
    {
        private readonly IAgeRepository _ageRepository;
        public AgeService(IAgeRepository ageRepository)
        {
            _ageRepository = ageRepository ?? throw new ArgumentNullException(nameof(ageRepository));
        }

        public async Task<IEnumerable<Age>> GetAllAges()
        {
            return await _ageRepository.GetAllAges();
        }
        public async Task<Age> CreateAge(Age age)
        {
            if (age == null) throw new ArgumentNullException(nameof(age));
            return await _ageRepository.CreateAge(age);
        }
        public async Task<Age> GetAgeById(int id)
        {
            return await _ageRepository.GetAgeById(id);
        }
        public async Task<bool> UpdateAge(int id, Age age)
        {
            if (age == null) throw new ArgumentNullException(nameof(age));
            return await _ageRepository.UpdateAge(id, age);
        }
        public async Task<bool> DeleteAge(int id)
        {
            return await _ageRepository.DeleteAge(id);
        }

    }
        
}
