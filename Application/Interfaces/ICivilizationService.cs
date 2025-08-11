using Application.Models.Dto;
using Application.Models.Request;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Enums;

namespace Application.Interfaces
{
    public interface ICivilizationService
    {
       Task<IEnumerable<CivilizationGalleryDto>> GetAllCivilization();
       Task<CivilizationDetailDto?> GetCivilizationById(int id);
       Task<CivilizationGalleryDto> CreateCivilization(CreateCivilizationRequest request);
        Task<CivilizationDetailDto> UpdateCivilizationAsync(int id, UpdateCivilizationRequest req);
        Task<bool> DeleteCivilization(int id);

    }
}
