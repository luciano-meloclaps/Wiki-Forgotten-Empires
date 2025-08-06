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
        IAsyncEnumerable<AgeAccordionDto> GetAgeDto();
        Task<Age> CreateAsync(CreateAgeDto dto);
        Task<AgeDetailDto?> GetAgeDetailById(int id);

        Task<Age> UpdateAgeDto(int id, Age age);
        Task<bool> DeleteAgeAsync(int id);
    }

}
