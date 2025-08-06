using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Models.Dto;
using Application.Models.Request;
using Application.Models.Request.Application.Models.Request;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application.Services
{
    public class AgeService : IAgeService
    {
        private readonly IAgeRepository _ageRepository;
        public AgeService(IAgeRepository ageRepository)
        {
            _ageRepository = ageRepository ?? throw new ArgumentNullException(nameof(ageRepository));
        }

        public async IAsyncEnumerable<AgeAccordionDto> GetAgeDto()
        {
            var ages = await _ageRepository.GetAllAges();
            foreach (var age in ages)
            {
                yield return new AgeAccordionDto
                {
                  
                    Name = age.Name,
                    Summary = age.Summary
                };
            }
        }
        public async Task<Age> CreateAsync(CreateAgeDto dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));
            var age = new Age
            {
                Name = dto.Name,
                Date = dto.Date,
                Overview = dto.Overview
            };
            return await _ageRepository.CreateAge(age);
        }
        public async Task<AgeDetailDto?> GetAgeDetailById(int id)
        {
            var age = await _ageRepository.GetAgeDetailById(id);
            return age is null ? null : AgeDetailDto.ToDto(age);
        }

        public async Task<Age> UpdateAgeDto(int id, Age age)
        {
            if (age == null) throw new ArgumentNullException(nameof(age));
            try
            {
                return await _ageRepository.PutDto(id, age);
            }
            catch (KeyNotFoundException ex)
            {
                // Log the exception or handle it as needed
                throw new Exception($"No se encuentra a la entidad Age: {id}.", ex);
            }
        }
        public async Task<bool> DeleteAgeAsync(int id)
        {
            try
            {
                return await _ageRepository.DeleteAgeAsync(id);
            }
            catch (DbUpdateException ex)
            {
                // Log the exception or handle it as needed
                throw new Exception($"Ocurrio un error al intentar eliminar la entidad AGE con ID: {id}.", ex);
            }
        }
    }
}