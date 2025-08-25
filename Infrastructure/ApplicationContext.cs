using Domain.Entities;
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
        // Constructor de la clase ApplicationContext que recibe las opciones de DbContext y un booleano para indicar si es un entorno de prueba
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
        { }

        // EF usa estos DbSet<> para mapear ent. -> tablas y relacionar
        //Tablas principales
        public DbSet<Character> Characters { get; set; }
        public DbSet<Civilization> Civilizations { get; set; }
        public DbSet<Battle> Battles { get; set; }
        public DbSet<Age> Ages { get; set; }

        //Tablas de relaciones N->N
        public DbSet<CharacterBattle> CharacterBattles { get; set; }
        public DbSet<CivilizationBattle> CivilizationBattles { get; set; }
        public DbSet<CivilizationAge> CivilizationPeriods { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //CharacterBattle N--N Character ⇄ Battle
            modelBuilder.Entity<CharacterBattle>()
                .HasKey(cb => new { cb.CharacterId, cb.BattleId });

            modelBuilder.Entity<CharacterBattle>()
                .HasOne(cb => cb.Character)
                .WithMany(c => c.Battles)
                .HasForeignKey(cb => cb.CharacterId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CharacterBattle>()
                .HasOne(cb => cb.Battle)
                .WithMany(b => b.Characters)
                .HasForeignKey(cb => cb.BattleId)
                .OnDelete(DeleteBehavior.Cascade);

            //CivilizationBattle N--N Civilization ⇄ Battle
            modelBuilder.Entity<CivilizationBattle>()
                .HasKey(cb => new { cb.CivilizationId, cb.BattleId });
            modelBuilder.Entity<CivilizationBattle>()
                .HasOne(cb => cb.Civilization)
                .WithMany(c => c.Battles)
                .HasForeignKey(cb => cb.CivilizationId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<CivilizationBattle>()
                .HasOne(cb => cb.Battle)
                .WithMany(b => b.Civilizations)
                .HasForeignKey(cb => cb.BattleId)
                .OnDelete(DeleteBehavior.Cascade);

            //CivilizationAge N--N Civilization ⇄ Age
            modelBuilder.Entity<CivilizationAge>()
                .HasKey(ca => new { ca.CivilizationId, ca.AgeId });
            modelBuilder.Entity<CivilizationAge>()
                .HasOne(ca => ca.Civilization)
                .WithMany(c => c.Ages)
                .HasForeignKey(ca => ca.CivilizationId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<CivilizationAge>()
                .HasOne(ca => ca.Age)
                .WithMany(a => a.Civilizations)
                .HasForeignKey(ca => ca.AgeId)
                .OnDelete(DeleteBehavior.Cascade);

            // Chacater N--1 Civilization
            modelBuilder.Entity<Character>()
                .HasOne(c => c.Civilization)
                .WithMany(civ => civ.Characters)
                .HasForeignKey(c => c.CivilizationId)
                .OnDelete(DeleteBehavior.Restrict);

            // Chacater N--1 Age
           modelBuilder.Entity<Character>()
                .HasOne(c => c.Age)
                .WithMany(a => a.Characters)
                .HasForeignKey(c => c.AgeId)
                .OnDelete(DeleteBehavior.Restrict);

            // Battle N--1 Age
            modelBuilder.Entity<Battle>()
                .HasOne(b => b.Age)
                .WithMany(a => a.Battles)
                .HasForeignKey(b => b.AgeId)
                .OnDelete(DeleteBehavior.Restrict); //Impide borrar una edad si tiene batallas
        }
    }
}
