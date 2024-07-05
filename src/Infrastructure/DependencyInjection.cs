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
            var connectionString = configuration.GetValue<string>("POSTGRES_CONSTR") ?? string.Empty;
            options.UseNpgsql(connectionString);
        });
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        return services;
    }
}