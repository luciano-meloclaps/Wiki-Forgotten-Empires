using Application.Models.Dto;
using Application.Models.Request;
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
        Task<IEnumerable<CivilizationDto>> GetAllCivilization();
        Task<CivilizationDto> CreateCivilization(CivilizationRequest req);
    }
}
