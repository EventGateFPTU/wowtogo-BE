using dotenv.net;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using UseCases.Common.Models;
using CorsPolicy = API.Common.CorsPolicy;

namespace API;

public static class DependencyInjection
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCors(options =>
        {
            options.AddPolicy( CorsPolicy.Development, builder =>
            {
                builder
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
            
            options.AddPolicy(CorsPolicy.Production, policy =>
            {
                policy.WithOrigins(configuration.GetValue<string>("CLIENT_ORIGIN_URL") ?? string.Empty)
                    .WithHeaders([
                        HeaderNames.ContentType,
                        HeaderNames.Authorization
                    ])
                    .AllowAnyMethod()
                    .SetPreflightMaxAge(TimeSpan.FromSeconds(86400));
            });
        });

        services.AddScoped<AuthMiddleware.AuthMiddleware>();
        services.AddScoped<CurrentUser>();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                var audience = configuration.GetValue<string>("AUTH0_AUDIENCE");

                options.Authority =
                    $"https://{configuration.GetValue<string>("AUTH0_DOMAIN")}/";
                options.Audience = audience;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true
                };
            });

        services.AddAuthorization();

        return services;
    }

    public static IHostBuilder AddHostConfigurations(this IHostBuilder host, IConfiguration configuration)
    {
        host.ConfigureAppConfiguration(configBuilder =>
        {
            configBuilder.Sources.Clear();
            DotEnv.Load();
            configBuilder.AddEnvironmentVariables();
        });

        return host;
    }

}