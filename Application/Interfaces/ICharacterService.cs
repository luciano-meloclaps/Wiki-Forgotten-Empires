using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Models.Dto;

namespace Application.Interfaces
{
    public interface ICharacterService
    {

        /* Task<IEnumerable<CharacterDto>> GetAllCharactersAsync();*/
        Task<CharacterDtoDetail> CharacterDetail(int id);
    }
}
