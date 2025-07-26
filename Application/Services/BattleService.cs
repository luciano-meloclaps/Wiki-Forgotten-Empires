using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
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
       
    }
}
