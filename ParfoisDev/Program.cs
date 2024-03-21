using Data.Repository;
using Data.Repository.Implementations;
using Data.Repository.Interfaces;
using Data.Services.Rules;

using ApplicationImplementations = Application.Services.Implementations;
using ApplicationInterfaces = Application.Services.Interfaces;
using DataImplementations = Data.Services.Implementations;
using DataInterfaces = Data.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApiContext>();

// Data.Repository
builder.Services.AddScoped<IPedidoRepository, PedidoRepository>();

// Data.Services
builder.Services.AddScoped<IRuleFactory, RuleFactory>();
builder.Services.AddScoped<IRule, AprovadoRule>();
builder.Services.AddScoped<IRule, ItemsAprovadosAMaiorRule>();
builder.Services.AddScoped<IRule, ItemsAprovadosAMenorRule>();
builder.Services.AddScoped<IRule, ValorAprovadoAMaiorRule>();
builder.Services.AddScoped<IRule, ValorAprovadoAMenorRule>();
builder.Services.AddScoped<IRule, ReprovadoRule>();
builder.Services.AddScoped<DataInterfaces.IPedidoService, DataImplementations.PedidoService>();
builder.Services.AddScoped<DataInterfaces.IStatusService, DataImplementations.StatusService>();

// Application.Services
builder.Services.AddScoped<ApplicationInterfaces.IPedidoService, ApplicationImplementations.PedidoService>();
builder.Services.AddScoped<ApplicationInterfaces.IStatusService, ApplicationImplementations.StatusService>();

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
