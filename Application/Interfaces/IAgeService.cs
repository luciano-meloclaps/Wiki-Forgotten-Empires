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

        Task<Age> UpdateAgeDto(int id, Age age);
        Task<bool> DeleteAgeAsync(int id);
    }

}
