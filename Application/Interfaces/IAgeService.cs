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
        Task<IEnumerable<AgeAccordionDto>> GetAllAges(CancellationToken ct);
        Task<AgeDetailDto?> GetAgeById(int id, CancellationToken ct);
        Task<AgeDetailDto> CreateAge(CreateAgeDto request, CancellationToken ct);
        Task<bool> UpdateAge(int id, UpdateAgeDto request, CancellationToken ct);
        Task<bool> DeleteAge(int id, CancellationToken ct);
    }
}
