﻿using System.ComponentModel.DataAnnotations;
using Domain.Entities;
using Domain.Enums;

namespace Application.Models.Request
{
    public class CharacterCreateRequest
    {
        [Required(ErrorMessage = "El campo nombre es obligatorio.")]
        [MinLength(5, ErrorMessage = "El campo nombre debe tener al menos 3 caracteres.")]
        public string Name { get; set; }

        public string? Description { get; set; }

        [Required(ErrorMessage = "Todo Personaje debe pertenecer a una Edad.")]
        public int AgeId { get; set; }

        public static Character ToEntity(CharacterCreateRequest req)
        {
            return new Character
            {
                Name = req.Name,
                Description = req.Description,
                AgeId = req.AgeId
            };
        }
    }

    public class CharacterUpdateRequest
    {
        [Required(ErrorMessage = "El campo Nombre es obligatorio.")]
        [MinLength(5, ErrorMessage = "El campo Nombre debe tener al menos 5 caracteres.")]
        public string? Name { get; set; }

        public string? Description { get; set; }
        public string? HonorificTitle { get; set; }
        public string? ImageUrl { get; set; }
        public string? LifePeriod { get; set; }
        public string? Dynasty { get; set; }
        public int? CivilizationId { get; set; }
        public int? AgeId { get; set; }
        public RoleCharacter? Role { get; set; }

        public static void ApplyToEntity(CharacterUpdateRequest req, Character character)
        {
            if (req.Name is not null) character.Name = req.Name;
            if (req.Description is not null) character.Description = req.Description;
            if (req.HonorificTitle is not null) character.HonorificTitle = req.HonorificTitle;
            if (req.ImageUrl is not null) character.ImageUrl = req.ImageUrl;
            if (req.LifePeriod is not null) character.LifePeriod = req.LifePeriod;
            if (req.Dynasty is not null) character.Dynasty = req.Dynasty;
            if (req.Role is not null) character.Role = req.Role;
            if (req.CivilizationId is not null) character.CivilizationId = req.CivilizationId.Value;
            if (req.AgeId is not null) character.AgeId = req.AgeId.Value;
            if (req.AgeId.HasValue) character.AgeId = req.AgeId.Value;
        }
    }
}