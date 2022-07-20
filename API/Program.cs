using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Specification;
using Services;
using Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;
const string allowAnyOrigin = "any";
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

services.AddCors(options =>
{
    options.AddPolicy(name: allowAnyOrigin, builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(allowAnyOrigin);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
