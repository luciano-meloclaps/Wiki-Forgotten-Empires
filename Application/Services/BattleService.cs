using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Models.Dto;
using Application.Models.Request;
using Domain.Entities;
using Domain.Interfaces;

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
        public async Task<BattleDto> CreateBattleAsync(BattleRequest req)
        {
            if (req == null) throw new ArgumentNullException(nameof(req));
            var battle = new Battle
            {
                Name = req.Name,
                Summary = req.Summary,
                Date = req.Date
            };
            var createdBattle = await _battleRepository.CreateAsync(battle);
            return new BattleDto
            {
                Name = createdBattle.Name,
                Summary = createdBattle.Summary,
                Date = createdBattle.Date
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
                Date = updatedBattle.Date
            };
        }
        public async Task<bool> DeleteBattleAsync(int id)
        {
            return await _battleRepository.DeleteAsync(id);
        }
    }
}
