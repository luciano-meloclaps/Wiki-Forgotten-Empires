﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public enum CivilizationTerritory
    {
        Europe,
        Asia,
        Africa,
        Americas,
        Oceania
    }
    public enum CivilizationState
    {
        Active,
        Dissolved
    }
    
    public class Civilization
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public CivilizationTerritory Territory { get; set; }
        public CivilizationState State { get; set; }
    }
}
