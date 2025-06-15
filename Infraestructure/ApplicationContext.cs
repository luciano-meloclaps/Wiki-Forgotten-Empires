using Domain.Entities;
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
        // Esta entidad seria traducir una entidad a tabla y viceversa, la interaccion 
        public DbSet<Battle> Battles { get; set; }
        private readonly bool isTestingEnvironment; // Corregido: "Enviroment" a "Environment"

        // Constructor de la clase ApplicationContext que recibe las opciones de DbContext y un booleano para indicar si es un entorno de prueba
        public ApplicationContext(DbContextOptions<ApplicationContext> options, bool isTestingEnvironment = false) : base(options)
        {
            this.isTestingEnvironment = isTestingEnvironment; // Corregido: "Enviroment" a "Environment"
        }
    }
}
