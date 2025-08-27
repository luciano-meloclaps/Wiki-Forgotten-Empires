using Application.Interfaces;
using Application.Models.Dto;
using Application.Models.Request;
using Domain.Interfaces;

namespace ForgottenEmpires.Application.Services;

public class AgeService : IAgeService
{
    private readonly IAgeRepository _ageRepository;

    public AgeService(IAgeRepository ageRepository)
    {
        _ageRepository = ageRepository;
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
}