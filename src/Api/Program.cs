using CompanyName.SampleApi.Infrastructure.Data;
using Lamar.Microsoft.DependencyInjection;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseLamar((_, registry) =>
{
    registry.Scan(scanner =>
    {
        scanner.AssembliesFromApplicationBaseDirectory(x => x.FullName!.Contains("CompanyName"));
        scanner.LookForRegistries();
        scanner.WithDefaultConventions();
        scanner.SingleImplementationsOfInterface();
    });
});

// Add services to the container.
builder.Services.AddDbContext<SalesDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SalesDbContext"));
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

public partial class Program { }