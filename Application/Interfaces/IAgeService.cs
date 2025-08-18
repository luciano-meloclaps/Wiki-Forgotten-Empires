using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Models.Dto;
using Application.Models.Request.Application.Models.Request;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IAgeService
    {
        Task<List<AgeAccordionDto>> GetAllAsync(CancellationToken ct = default);
        Task<AgeDetailDto?> GetAgeDetailByIdAsync(int id, CancellationToken ct = default);

        Task<AgeDetailDto> CreateFromDtoAsync(CreateAgeDto dto, CancellationToken ct = default);
        Task<AgeDetailDto> UpdateAsync(int id, UpdateAgeDto dto, CancellationToken ct = default);
        Task<bool> DeleteAsync(int id, CancellationToken ct = default);
    }

}
