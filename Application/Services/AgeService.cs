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
        public async IAsyncEnumerable<Age> GetAgeDto()
        {
            var ages = await _ageRepository.GetAllAges();
            foreach (var age in ages)
            {
                yield return age;
            }
        }
        public async Task<Age> CreateAgeDto(Age age)
        {
            if (age == null) throw new ArgumentNullException(nameof(age));
            return await _ageRepository.CreateAge(age);
        }
    }
    }
