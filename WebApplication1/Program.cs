using Application.Interfaces;
using Application.Services;
using Domain.Interfaces;
using ForgottenEmpires.Application.Services;
using Infrastructure;
using Infrastructure.Repositories;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Configure DbContext with SQLite /
var connection = new SqliteConnection("Data Source=ForgottenEmpire.db");
connection.Open();

//Set journal mode to DELETE using PRAGMA statement
using (var command = connection.CreateCommand())
{
    command.CommandText = "PRAGMA journal_mode=DELETE;"; //Para que no nos cree mas de un archivo cuando persisnte los objetos
    command.ExecuteNonQuery();
}

//Registrar el Context en el contenedor de serivicos
builder.Services.AddDbContext<ApplicationContext>(DbContextOptions =>
    DbContextOptions.UseSqlite(connection));

// Alternative way to configure the DbContext with a connection string from appsettings.json CONEXION ANTERIOR
/*builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("ForgottenEmpireBDConnectionString")));*/

//Age
builder.Services.AddScoped<IAgeRepository, AgeRepository>();
builder.Services.AddScoped<IAgeService, AgeService>();

//Civilization
builder.Services.AddScoped<ICivilizationRepository, CivilizationRepository>();
builder.Services.AddScoped<ICivilizationService, CivilizationService>();

// Battle
builder.Services.AddScoped<IBattleRepository, BatlleRepository>();
builder.Services.AddScoped<IBattleService, BattleService>();

//Character
builder.Services.AddScoped<ICharacterRepository, CharacterRepository>();
builder.Services.AddScoped<ICharacterService, CharacterService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();