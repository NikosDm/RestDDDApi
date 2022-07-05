using Microsoft.EntityFrameworkCore;
using RestDDDApi.Api.Data;
using RestDDDApi.Domain.Interfaces;
using RestDDDApi.Infrastructure.Database;
using RestDDDApi.Infrastructure.Domain.UnitOfWork;
using RestDDDApi.Api.Queries.Handlers;
using RestDDDApi.Api.Commands.Handlers;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<CustomerQueryHandler>();
builder.Services.AddScoped<ProductQueryHandler>();
builder.Services.AddScoped<CustomerCommandHandler>();
builder.Services.AddScoped<ProductCommandHandler>();
// builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),  b => b.MigrationsAssembly("RestDDDApi.Api")));
builder.Services.AddDbContext<DataContext>(options => options.UseInMemoryDatabase(databaseName: "RestDDDApiTest"));
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Rest DDD Api project",
    });
    
    // using System.Reflection;
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
    
var app = builder.Build();

try 
{
    using var scope = app.Services.CreateScope();
    var dataContext = scope.ServiceProvider.GetRequiredService<DataContext>();
    // await dataContext.Database.MigrateAsync();
    await Seed.SeedData(dataContext);
}
catch(Exception ex)
{
    var logger = app.Services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occurred during migration.");
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "DatingAppApi v1");
    });
}

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();
