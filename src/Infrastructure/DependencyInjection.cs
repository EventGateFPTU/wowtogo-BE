using Domain.Interfaces.Data;
using Domain.Interfaces.Services;
using Infrastructure.Data;
using Infrastructure.Services;
using Infrastructure.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenFga.Sdk.Client;
using UseCases.Common.Contracts;
using UseCases.Common.Shared;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<WowToGoDBContext>(options =>
        {
            var connectionString = configuration.GetValue<string>("POSTGRES_CONSTR") ?? string.Empty;
            options.UseNpgsql(connectionString);
            options.EnableSensitiveDataLogging();
        });
        services.AddScoped<IAuth0Service, Auth0Service>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddAuth0(configuration);
        services.AddScoped<PipelineContext>();
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddOpenFGA(configuration);

        return services;
    }

    public static IServiceCollection AddAuth0(this IServiceCollection services, IConfiguration configuration)
    {
        var auth0Domain = configuration.GetValue<string>("AUTH0_DOMAIN");
        var auth0Audience = configuration.GetValue<string>("AUTH0_EVIDENCE");
        services.Configure<Auth0Settings>((model) =>
        {
            model.Domain = auth0Domain!;
            model.Audience = auth0Audience!;
        });

        return services;
    }

    public static IServiceCollection AddOpenFGA(this IServiceCollection services, IConfiguration configuration)
    {
        var apiUrl = configuration.GetValue<string>("FGA_API_URL");
        var storeId = configuration.GetValue<string>("FGA_STORE_ID");
        var modelId = configuration.GetValue<string>("FGA_MODEL_ID");
        services.Configure<OpenFgaSettings>(model =>
        {
            model.ApiUrl = apiUrl!;
            model.StoreId = storeId!;
            model.ModelId = modelId!;
        });

        services.AddScoped<OpenFgaClient>(_ =>
        {
            var config = new ClientConfiguration
            {
                ApiUrl = apiUrl!,
                StoreId = storeId,
                AuthorizationModelId = modelId,
            };
            return new OpenFgaClient(config);
        });

        services.AddScoped<IPermissionManager, PermissionManager>();

        return services;
    }
}