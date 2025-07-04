﻿using Domain.Entities;
using Domain.Relations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class ApplicationContext : DbContext
    {
        // EF usa estos DbSet<> para mapear ent. -> tablas y relacionar,
        public DbSet<Character> Characters { get; set; }
        public DbSet<Civilization> Civilizations { get; set; }
        public DbSet<Battle> Battles { get; set; }
        public DbSet<Age> Ages { get; set; }

        public DbSet<CharacterBattle> CharacterBattles { get; set; }
        public DbSet<CivilizationBattle> CivilizationBattles { get; set; }
        public DbSet<CivilizationAge> CivilizationPeriods { get; set; }


        private readonly bool isTestingEnvironment; // Corregido: "Enviroment" a "Environment"

        // Constructor de la clase ApplicationContext que recibe las opciones de DbContext y un booleano para indicar si es un entorno de prueba
        public ApplicationContext(DbContextOptions<ApplicationContext> options, bool isTestingEnvironment = false) : base(options)
        {
            this.isTestingEnvironment = isTestingEnvironment; // Corregido: "Enviroment" a "Environment"
        }
    }
}
