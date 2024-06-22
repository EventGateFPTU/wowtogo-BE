using API.Authorization;
using API.Extensions;
using dotenv.net;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;

namespace API;

public static class DependencyInjection
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
            {
                policy.WithOrigins(configuration.GetValue<string>("CLIENT_ORIGIN_URL") ?? string.Empty)
                    .WithHeaders([
                        HeaderNames.ContentType,
                        HeaderNames.Authorization
                    ])
                    .WithMethods("GET")
                    .SetPreflightMaxAge(TimeSpan.FromSeconds(86400));
            });
        });

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

        services.RegisterPolicies();

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