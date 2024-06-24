using Domain.Interfaces.Data;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<WowToGoDBContext>(options =>
        {
            options.UseNpgsql("User ID=postgres;Password=12345;Host=localhost;Port=5432;Database=WowToGoDB;Pooling=true;Connection Lifetime=0;");
        });
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        return services;
    }
}