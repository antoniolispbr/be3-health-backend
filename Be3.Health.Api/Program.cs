using Be3.Health.Application.Interfaces;
using Be3.Health.Application.Services;
using Be3.Health.Domain.Interfaces;
using Be3.Health.Infra.Data;
using Be3.Health.Infra.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// -----------------------------------------------------------
// 🔥 CONFIGURAÇÃO DE CORS — Angular (localhost:4200)
// -----------------------------------------------------------
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular",
        policy =>
        {
            policy
                .WithOrigins("http://localhost:4200") // Angular
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

// -----------------------------------------------------------
// Controllers / API
// -----------------------------------------------------------
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// -----------------------------------------------------------
// EF Core
// -----------------------------------------------------------
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

// -----------------------------------------------------------
// Injeção de dependência - PACIENTES
// -----------------------------------------------------------
builder.Services.AddScoped<IPacienteRepository, PacienteRepository>();
builder.Services.AddScoped<IPacienteService, PacienteService>();

// -----------------------------------------------------------
// Injeção de dependência - CONVÊNIOS
// -----------------------------------------------------------
builder.Services.AddScoped<IConvenioRepository, ConvenioRepository>();
builder.Services.AddScoped<IConvenioService, ConvenioService>();

// -----------------------------------------------------------
var app = builder.Build();
// -----------------------------------------------------------

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// -----------------------------------------------------------
// 🔥 ATIVAR CORS ANTES DO Authorization e antes do MapControllers()
// -----------------------------------------------------------
app.UseCors("AllowAngular");

app.UseAuthorization();

app.MapControllers();

app.Run();
