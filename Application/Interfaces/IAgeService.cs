using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Models.Dto;
using Application.Models.Request;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IAgeService
    {
        Task<List<AgeAccordionDto>> GetAllAsync(CancellationToken ct);
        Task<AgeDetailDto?> GetByIdAsync(int id, CancellationToken ct);
        Task<AgeAccordionDto> CreateAsync(CreateAgeDto dto, CancellationToken ct);
        Task<AgeAccordionDto?> UpdateAsync(int id, UpdateAgeDto dto, CancellationToken ct);
        Task<(bool Eliminado, string? Nombre)> DeleteAsync(int id, CancellationToken ct);
    }
}
