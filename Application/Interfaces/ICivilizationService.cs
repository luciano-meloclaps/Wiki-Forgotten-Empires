using Application.Models.Dto;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICivilizationService
    {
        public Task<IEnumerable<CivilizationDto>> GetAllCivilization();
        public Task<CivilizationDto> CreateCivilization(CivilizationDto civilizationDto);
    }
}
