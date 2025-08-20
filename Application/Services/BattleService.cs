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
using Domain.Relations;
using Microsoft.EntityFrameworkCore;
using static Application.Models.Dto.BattleTableDto;
using static Application.Models.Request.CreateBattleDto;

namespace Application.Services
{
    public class BattleService : IBattleService
    {
        private readonly IBattleRepository _battleRepository;
        public BattleService(IBattleRepository battleRepository)
        {
            _battleRepository = battleRepository ?? throw new ArgumentNullException(nameof(battleRepository));
        }
        public async Task<List<BattleTableDto>> GetAllBattles(CancellationToken ct)
        {
            var battles = await _battleRepository.GetAll(ct);
            return battles.Select(BattleTableDto.ToDto).ToList();
        }

        public async Task<BattleDetailDto?> GetBattleById(int id, CancellationToken ct)
        {
            var battle = await _battleRepository.GetByIdAsync(id, ct);
            return battle is null ? null : BattleDetailDto.ToDto(battle);
        }

        public async Task<BattleDetailDto> CreateBattle(CreateBattleDto dto, CancellationToken ct)
        {
            var battle = CreateBattleDto.ToEntity(dto);
            await _battleRepository.CreateAsync(battle, ct); 
            await _battleRepository.SaveChangesAsync(ct);   
            return BattleDetailDto.ToDto(battle);
        }
        //DRY: Don't Repeat Yourself a diferncia de Age se usa un Helper para hacer DPE
        public async Task<BattleDetailDto?> UpdateBattle(int id, UpdateBattleDto dto, CancellationToken ct)
        {
            var battle = await _battleRepository.UpdateAsync(id, ct);
            if (battle is null) return null;

            UpdateBattleDto.ApplyToEntity(dto, battle);

            await _battleRepository.SaveChangesAsync(ct);
            return BattleDetailDto.ToDto(battle);
        }

        public async Task<(bool Eliminado, string? Nombre)> DeleteBattle(int id, CancellationToken ct)
        {
            var (deletedBattle, nameBattle) = await _battleRepository.DeleteAsync(id, ct);
            return (deletedBattle, nameBattle);
        }
    }
}

