using API.Controllers;
using Infrastructure.Postgres.Scaffolding;
using Microsoft.EntityFrameworkCore;

namespace API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        ConfigureServices(builder.Services);

        var app = builder.Build();

        using (var scope = app.Services.CreateScope())
        {
            var ctx = scope.ServiceProvider.GetRequiredService<MyDbContext>();
            ctx.Database.EnsureCreated();
        }

        app.MapControllers();
        
        app.Run();
    }

    public static void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<MyDbContext>(options =>
        {
            options.UseSqlite("Data Source=pets.db");
        });
        services.AddScoped<PetService>();
        
        services.AddControllers();
    }
}