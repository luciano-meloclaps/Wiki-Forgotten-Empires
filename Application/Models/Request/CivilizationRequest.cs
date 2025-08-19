﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Enums;

namespace Application.Models.Request
{
    public class CreateCivilizationRequest
    {
        [Required(ErrorMessage = "El Nombre es obligatorio.")]
        public string Name { get; set; }

        public TerritoryType Territory { get; set; } = TerritoryType.None;
        public CivilizationState State { get; set; } = CivilizationState.None;

        public static Civilization ToEntity(CreateCivilizationRequest req)
        {
            return new Civilization
            {
                Name = req.Name,
                Territory = req.Territory,
                State = req.State,
            };
        }
    }
    public class UpdateCivilizationRequest
    {
        public string? Name { get; set; }
        public string? Summary { get; set; }
        public string? Overview { get; set; }
        public string? ImageUrl { get; set; }
        public TerritoryType? Territory { get; set; }
        public CivilizationState? State { get; set; }
        public static void ApplyToEntity(UpdateCivilizationRequest req, Civilization civilization)
        {
            if (req.Name is not null) civilization.Name = req.Name;
            if (req.Summary is not null) civilization.Summary = req.Summary;
            if (req.Overview is not null) civilization.Overview = req.Overview;
            if (req.ImageUrl is not null) civilization.ImageUrl = req.ImageUrl;
            if (req.Territory is not null) civilization.Territory = req.Territory.Value;
            if (req.State is not null) civilization.State = req.State.Value;
        }
    }
}

