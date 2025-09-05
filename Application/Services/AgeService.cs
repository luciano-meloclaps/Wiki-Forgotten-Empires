using Application.Interfaces;
using Application.Models.Dto;
using Application.Models.Request;
using Domain.Interfaces;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace ForgottenEmpires.Application.Services;

public class AgeService : IAgeService
{
    private readonly IAgeRepository _ageRepository;
    private readonly ApplicationContext _context;

    public AgeService(IAgeRepository ageRepository, ApplicationContext context)
    {
        _ageRepository = ageRepository;
        _context = context;
    }

    public async Task<IEnumerable<AgeAccordionDto>> GetAllAges(CancellationToken ct)
    {
        var ages = await _ageRepository.GetAllAges(ct);
        return ages.Select(AgeAccordionDto.ToDto);
    }

    public async Task<AgeDetailDto?> GetAgeById(int id, CancellationToken ct)
    {
        var age = await _ageRepository.GetAgeById(id, ct);
        return age is null ? null : AgeDetailDto.ToDto(age);
    }

    public async Task<AgeDetailDto> CreateAge(CreateAgeDto request, CancellationToken ct)
    {
        var age = CreateAgeDto.ToEntity(request);
        await _ageRepository.CreateAge(age, ct);
        return AgeDetailDto.ToDto(age);
    }

    public async Task<bool> UpdateAge(int id, UpdateAgeDto request, CancellationToken ct)
    {
        var age = await _ageRepository.GetAgeById(id, ct);
        if (age is null)
        {
            return false;
        }

        UpdateAgeDto.ApplyToEntity(request, age);
        await _ageRepository.UpdateAge(age, ct);
        return true;
    }

    public async Task<bool> DeleteAge(int id, CancellationToken ct)
    {
        var age = await _ageRepository.GetAgeById(id, ct);
        if (age is null)
        {
            return false;
        }
        await _ageRepository.DeleteAge(age, ct);
        return true;
    }

    public async Task<(bool Success, string ErrorMessage)> UpdateAgeRelations(int id, UpdateAgeRelationsDto dto, CancellationToken ct)
    {
        var ageExists = await _context.Ages.AnyAsync(a => a.Id == id, ct);
        if (!ageExists)
        {
            return (false, $"No se encontró la Age con id {id}.");
        }

        if (dto.BattleId.HasValue)
        {
            var battle = await _context.Battles.FindAsync(dto.BattleId.Value);
            if (battle is null) return (false, $"No se encontró la Battle con id {dto.BattleId.Value}.");

            battle.AgeId = id; // Asigna la relación
            _context.Update(battle);
        }

        if (dto.CharacterId.HasValue)
        {
            var character = await _context.Characters.FindAsync(dto.CharacterId.Value);
            if (character is null) return (false, $"No se encontró el Character con id {dto.CharacterId.Value}.");

            character.AgeId = id; // Asigna la relación
            _context.Update(character);
        }

        await _context.SaveChangesAsync(ct);
        return (true, string.Empty);
    }

    public async Task<(bool Success, string ErrorMessage)> RemoveAgeRelationsAsync(int ageId, UpdateAgeRelationsDto dto, CancellationToken ct)
    {
        if (!dto.CharacterId.HasValue && !dto.BattleId.HasValue)
        {
            return (false, "Debe proporcionar al menos un CharacterId o BattleId para eliminar la relación.");
        }

        // Desvincular Character
        if (dto.CharacterId.HasValue)
        {
            var character = await _context.Characters.FirstOrDefaultAsync(c => c.Id == dto.CharacterId.Value, ct);
            if (character is null) return (false, $"No se encontró el Character con id {dto.CharacterId.Value}.");

            if (character.AgeId != ageId) return (false, $"El Character {dto.CharacterId.Value} no pertenece a la Age {ageId}.");

            character.AgeId = null;
            _context.Update(character);
        }

        // Desvincular Battle
        if (dto.BattleId.HasValue)
        {
            var battle = await _context.Battles.FirstOrDefaultAsync(b => b.Id == dto.BattleId.Value, ct);
            if (battle is null) return (false, $"No se encontró la Battle con id {dto.BattleId.Value}.");

            if (battle.AgeId != ageId) return (false, $"La Battle {dto.BattleId.Value} no pertenece a la Age {ageId}.");

            battle.AgeId = null;
            _context.Update(battle);
        }

        await _context.SaveChangesAsync(ct);
        return (true, string.Empty);
    }
}