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
        public async Task<IEnumerable<BattleTableDto>> GetBattleTable()
        {
            var battles = await _battleRepository.GetAllBattles();
            return battles.Select(BattleTableDto.ToDto);
        }

        public async Task<BattleDetailDto?> GetBattleDetail(int id)
        {
            var battle = await _battleRepository.GetBattleById(id);
            return battle is null
                ? null
                : BattleDetailDto.ToDto(battle);
        }

        public async Task<Battle> CreateBattle(CreateBattleDto dto)
        {
            var battle = CreateBattleDto.ToEntity(dto);
            return await _battleRepository.CreateBattle(battle);
        }

        public async Task<Battle?> UpdateBattle(int id, UpdateBattleDto dto)
        {
            var battle = await _battleRepository.GetBattleById(id);
            if (battle is null) return null;

            UpdateBattleDto.ApplyToEntity(dto, battle);
            return await _battleRepository.UpdateBattle(battle);
        }

        public async Task<bool> DeleteBattle(int id)
        {
            return await _battleRepository.DeleteBattleAsync(id);
        }


    }
}
