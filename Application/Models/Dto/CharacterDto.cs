using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Models.Dto
{
    public class CharacterDto
    {
        public string Name { get; set; }
        public string HonorificTitle { get; set; }
        public string ImageUrl { get; set; }
        public string LifePeriod { get; set; }

        public CharacterDto ToDto(Character character)
        {
            return new CharacterDto
            {
                Name = Name,
                HonorificTitle = HonorificTitle,
                ImageUrl = ImageUrl,
                LifePeriod = LifePeriod
            };
        }
    }
    }


