using Microsoft.EntityFrameworkCore;
using RestDDDApi.Domain.Interfaces;
using RestDDDApi.Infrastructure.Database;
using RestDDDApi.Infrastructure.Domain.UnitOfWork;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),  b => b.MigrationsAssembly("RestDDDApi.Api")));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
    
var app = builder.Build();

try 
{
    using var scope = app.Services.CreateScope();
    var dataContext = scope.ServiceProvider.GetRequiredService<DataContext>();
    await dataContext.Database.MigrateAsync();
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
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DatingAppApi v1"));
}

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();
