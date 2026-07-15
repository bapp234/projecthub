using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProjectHub.Application.Interfaces;
using ProjectHub.Domain.Interfaces;
using ProjectHub.Persistence.Authentication;
using ProjectHub.Persistence.Database;
using ProjectHub.Persistence.Repositories;
using Microsoft.Extensions.Options;
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
        services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.SectionName));
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IPasswordHasher, BCryptPasswordHasher>();
        services.AddScoped<IJwtProvider, JwtProvider>();
        return services;
    }
}