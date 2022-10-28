using Application.Exceptions;
using Application.Infrastructure;
using DataAccess;
using DataAccess.SeedData;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();

builder.Services.AddDbContext<BeerQuestDbContext>(options 
    => options.UseSqlServer(builder.Configuration["ConnectionStrings:BeerQuestDatabaseConnectionString"]));
builder.Services.AddScoped<DbContext, BeerQuestDbContext>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDataAccessServices()
                .AddApplicationServices();

var app = builder.Build();
    
if (app.Environment.IsDevelopment())
{
    using (var scope = app.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<BeerQuestDbContext>();
        
        db.Database.Migrate();

        db.Seed();
    }

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlingMiddleware>(Array.Empty<string>());

app.UseAuthorization();
app.MapControllers();
app.Run();