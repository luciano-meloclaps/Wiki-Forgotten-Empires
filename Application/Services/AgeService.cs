using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Models.Dto;
using Application.Models.Request;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class AgeService : IAgeService
    {
        private readonly IAgeRepository _ageRepository;
        public AgeService(IAgeRepository ageRepository)
        {
            _ageRepository = ageRepository ?? throw new ArgumentNullException(nameof(ageRepository));
        }
        public async Task<List<AgeAccordionDto>> GetAllAsync(CancellationToken ct)
        {
            var ages = await _ageRepository.GetAllAsync(ct);
            return ages.Select(AgeAccordionDto.ToDto).ToList();
        }
        public async Task<AgeDetailDto?> GetByIdAsync(int id, CancellationToken ct)
        {
            var age = await _ageRepository.GetByIdAsync(id, ct);
            return age == null ? null : AgeDetailDto.ToDto(age);
        }
        public async Task<AgeAccordionDto> CreateAsync(CreateAgeDto dto, CancellationToken ct)
        {
            var age = CreateAgeDto.ToEntity(dto);
            await _ageRepository.CreateAsync(age, ct);
            await _ageRepository.SaveChangesAsync(ct);
            return AgeAccordionDto.ToDto(age);
        }
        public async Task<AgeAccordionDto?> UpdateAsync(int id, UpdateAgeDto dtoUpdate, CancellationToken ct)
        {
            var age = await _ageRepository.GetByIdAsync(id, ct);
            if (age == null) return null;

            if (dtoUpdate.Name != null) age.Name = dtoUpdate.Name;
            if (dtoUpdate.Summary != null) age.Summary = dtoUpdate.Summary;
            if (dtoUpdate.Date != null) age.Date = dtoUpdate.Date;
            if (dtoUpdate.Overview != null) age.Overview = dtoUpdate.Overview;

            await _ageRepository.SaveChangesAsync(ct);
            return AgeAccordionDto.ToDto(age);
        }
        public async Task<(bool Eliminado, string? Nombre)> DeleteAsync(int id, CancellationToken ct)
        {
            var (dtoDelete, nombre) = await _ageRepository.DeleteAsync(id, ct);
            if (dtoDelete)
                await _ageRepository.SaveChangesAsync(ct);

            return (dtoDelete, nombre);
        }
    }
}
