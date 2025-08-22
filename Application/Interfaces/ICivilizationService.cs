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
        Task<IEnumerable<CivilizationGalleryDto>> GetAllCivilization(CancellationToken ct);
        Task<CivilizationDetailDto?> GetCivizlizationById(int id, CancellationToken ct);
        Task<Civilization> CreateCivilization(CreateCivilizationRequest request, CancellationToken ct);
        Task UpdateCivilization(int id, UpdateCivilizationRequest request, CancellationToken ct);
        Task DeleteCivilization(int id, CancellationToken ct);
    }
}
