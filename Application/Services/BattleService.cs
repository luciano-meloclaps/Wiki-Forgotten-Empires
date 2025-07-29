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


namespace Application.Services
{
    public class BattleService : IBattleService
    {
        private readonly IBattleRepository _battleRepository;
        public BattleService(IBattleRepository battleRepository)
        {
            _battleRepository = battleRepository ?? throw new ArgumentNullException(nameof(battleRepository));
        }
        public async Task<IEnumerable<BattleDto>> GetAllBattlesAsync()
        {
            var battles = await _battleRepository.GetAllAsync();
            return battles.Select(b => new BattleDto
            {
                Name = b.Name,
                Summary = b.Summary,
                Date = b.Date
            });
        }
        public async Task<BattleDto> GetBattleByIdAsync(int id)
        {
            var battle = await _battleRepository.GetByIdAsync(id);
            if (battle == null) return null;
            return new BattleDto
            {
                Name = battle.Name,
                Summary = battle.Summary,
                Date = battle.Date
            };
        }
        public async Task<BattleDto> CreateBattleAsync(BattleDto battleDto)
        {
            if (battleDto == null) throw new ArgumentNullException(nameof(battleDto));
            var battle = new Battle
            {
                Name = battleDto.Name,
                Summary = battleDto.Summary,
                Date = battleDto.Date,
                Territory = battleDto.Territory
            };
            var createdBattle = await _battleRepository.CreateAsync(battle);
            return new BattleDto
            {
                Name = createdBattle.Name,
                Summary = createdBattle.Summary,
                Date = createdBattle.Date,
                Territory = createdBattle.Territory
            };
        }
        public async Task<BattleDto> UpdateBattleAsync(int id, BattleRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            var battle = await _battleRepository.GetByIdAsync(id);
            if (battle == null) return null;
            battle.Name = request.Name;
            battle.Summary = request.Summary;
            battle.Date = request.Date;
            var updatedBattle = await _battleRepository.UpdateAsync(battle);
            return new BattleDto
            {
                Name = updatedBattle.Name,
                Summary = updatedBattle.Summary,
                Date = updatedBattle.Date,
                Territory = updatedBattle.Territory
            };
        }
        public async Task<bool> DeleteBattleAsync(int id)
        {
            return await _battleRepository.DeleteAsync(id);
        }
    }
}
