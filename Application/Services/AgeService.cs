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

        public async Task<AgeDetailDto> UpdateAsync(int id, UpdateAgeDto dto, CancellationToken ct = default)
        {
            if (dto is null)
                throw new ArgumentException("No puede ser NULL");

            var entity = UpdateAgeDto.ToEntity(dto);

            var updated = await _ageRepository.UpdateAsync(id, entity, ct);

            return AgeDetailDto.ToDto(updated);
        }


        public async Task<bool> DeleteAsync(int id, CancellationToken ct = default)
        {
            if (id <= 0)
                throw new ArgumentException("El id debe ser mayor a cero.", nameof(id));

            try
            {
                return await _ageRepository.DeleteAsync(id, ct);
            }
            catch (DbUpdateException ex)
            {
                throw new InvalidOperationException(
                    "No se puede eliminar la edad porque tiene relaciones asociadas.",
                    ex
                );
            }
        }



    }
}