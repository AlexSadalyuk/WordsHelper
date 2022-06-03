using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Specification;
using Services;
using Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;
// Add services to the container.

services.AddControllers()
        .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();


services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
services.AddScoped(typeof(ISpecificationBuilder<>), typeof(SpecificationBuilder<>));
services.AddScoped<IUnitOfWork, UnitOfWork>();
services.AddScoped<IExerciseService, ExerciseService>();
services.AddDbContext<WordsDbContext>(opts => opts.UseSqlServer(configuration.GetConnectionString("WordsDB")));

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
