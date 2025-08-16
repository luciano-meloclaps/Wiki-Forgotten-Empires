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

        public async Task<List<AgeAccordionDto>> GetAllAsync(CancellationToken ct = default)
        {
            var entities = await _ageRepository.GetAllAsync(ct);
            return entities.Select(AgeAccordionDto.ToDto).ToList();
        }

        public async Task<AgeDetailDto?> GetAgeDetailByIdAsync(int id, CancellationToken ct = default)
        {
            var age = await _ageRepository.GetAgeDetailByIdAsync(id, ct);
            return age is null ? null : AgeDetailDto.ToDto(age);
        }

        public async Task<AgeDetailDto> CreateFromDtoAsync(CreateAgeDto dto, CancellationToken ct = default)
        {
            if (dto is null)
                throw new ArgumentException("El objeto AgeDto no puede ser nulo.");
            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new ArgumentException("El nombre es obligatorio.");
            if (dto.Summary?.Length > 150)
                throw new ArgumentException("El resumen no debe superar los 150 caracteres.");

            var entity = CreateAgeDto.ToEntity(dto);
            var created = await _ageRepository.CreateAgeAsync(entity, ct);

            return AgeDetailDto.ToDto(created);
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