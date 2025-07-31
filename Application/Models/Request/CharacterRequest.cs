using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Enums;

namespace Application.Models.Request
{
    public class CharacterCreateRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public Character ToEntity (CharacterCreateRequest req)
            {
            return new Character
            {
                Name = req.Name,
                Description = req.Description
            };

        }
    }
    public class CharacterUpdateRequest
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? HonorificTitle { get; set; }
        public string? ImageUrl { get; set; }
        public string? LifePeriod { get; set; }
        public int? CivilizationId { get; set; }
        public int? AgeId { get; set; }
        public RoleCharacter? Role { get; set; }

        public Character ToEntity(CharacterUpdateRequest req)
        {
            return new Character
            {
                Name = req.Name,
                Description = req.Description,
                HonorificTitle = req.HonorificTitle,
                ImageUrl = req.ImageUrl,
                LifePeriod = req.LifePeriod,
                CivilizationId = req.CivilizationId ?? 0, 
                AgeId = req.AgeId ?? 0,
                Role = req.Role 
            };
        }
    }
}
