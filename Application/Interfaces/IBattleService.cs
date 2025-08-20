using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Models.Dto;
using Application.Models.Request;
using Domain.Entities;
using static Application.Models.Dto.BattleTableDto;
using static Application.Models.Request.CreateBattleDto;

namespace Application.Interfaces
{
    public interface IBattleService
    {
        Task<List<BattleTableDto>> GetAllBattles(CancellationToken ct);
        Task<BattleDetailDto?> GetBattleById(int id, CancellationToken ct);
        Task<BattleDetailDto> CreateBattle(CreateBattleDto dto, CancellationToken ct);
        Task<BattleDetailDto?> UpdateBattle(int id, UpdateBattleDto dto, CancellationToken ct);
        Task<(bool Eliminado, string? Nombre)> DeleteBattle(int id, CancellationToken ct);
    }

}

