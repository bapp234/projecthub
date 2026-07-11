using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProjectHub.Persistence.Database;
using ProjectHub.Application.Interfaces;
namespace ProjectHub.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<ProjectHubDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });
        services.AddScoped<IUserRepository, IUserRepository>();
        return services;
    }
}