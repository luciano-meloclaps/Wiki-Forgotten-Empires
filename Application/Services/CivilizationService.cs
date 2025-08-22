using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Models.Dto;
using Application.Models.Request;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using static Application.Models.Dto.BattleTableDto;



namespace Application.Services
{
    public class CivilizationService : ICivilizationService
    {
        private readonly ICivilizationRepository _civilizationRepository;
        public CivilizationService(ICivilizationRepository civilizationRepository)
        {
            _civilizationRepository = civilizationRepository;
        }

        public async Task<IEnumerable<CivilizationGalleryDto>> GetAllCivilization(CancellationToken ct)
        {
            var civilizations = await _civilizationRepository.GetAllCivilization(ct);
            return civilizations.Select(CivilizationGalleryDto.ToDto);
        }

        public async Task<CivilizationDetailDto?> GetCivizlizationById(int id, CancellationToken ct)
        {
            var civilization = await _civilizationRepository.GetCivizlizationById(id, ct);
            return civilization is null ? null : CivilizationDetailDto.ToDto(civilization);
        }

        public async Task<Civilization> CreateCivilization(CreateCivilizationRequest request, CancellationToken ct)
        {
            var civilization = CreateCivilizationRequest.ToEntity(request);
            return await _civilizationRepository.CreateCivilization(civilization, ct);
        }


        public async Task UpdateCivilization(int id, UpdateCivilizationRequest request, CancellationToken ct)
        {
            var civilization = await _civilizationRepository.GetCivizlizationById(id, ct);
             await _civilizationRepository.UpdateCivilization(civilization, ct);
        }
        public async Task DeleteCivilization(int id, CancellationToken ct)
        {
            var civilization = await _civilizationRepository.GetCivizlizationById(id, ct);
             await _civilizationRepository.DeleteCivilization(civilization, ct);
        }
    }
}