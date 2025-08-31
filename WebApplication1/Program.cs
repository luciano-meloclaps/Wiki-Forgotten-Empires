using Application.Interfaces;
using Application.Services;
using Domain.Interfaces;
using ForgottenEmpires.Application.Services;
using Infrastructure;
using Infrastructure.Repositories;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//  Swagger => Añadir un header de nombre Authorize con el siguiente valor Bearer
builder.Services.AddSwaggerGen(setupAction =>
{
    setupAction.AddSecurityDefinition("ApiBearerAuth", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        Description = "Acá pega el token generado al loguearse."
    });

    setupAction.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "ApiBearerAuth"
                }
            },
            new List<string>()
        }
    });
});

//Configuración de JWT
//Linea 52 a 54: Chequeos JWT de la Request
//Linea 55 a 58: Contra que comparamos para validar el token
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Authentication:Issuer"],
            ValidAudience = builder.Configuration["Authentication:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(
                    builder.Configuration["Authentication:SecretForKey"]
                )
            )
        };
    });

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

//Para que las Request pasen por el middleware de autenticación
app.UseAuthentication();

app.MapControllers();

app.Run();