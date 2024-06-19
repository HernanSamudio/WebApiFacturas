using Microsoft.EntityFrameworkCore;
using WebApiFacturas.Repository;
using WebApiFacturas.Services;
using FluentValidation.AspNetCore;
using WebApiFacturas.Validations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Program>());

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ClienteService>();
builder.Services.AddScoped<FacturaService>();
builder.Services.AddScoped<ClienteRepository>();
builder.Services.AddScoped<FacturaRepository>();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Scoped);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();