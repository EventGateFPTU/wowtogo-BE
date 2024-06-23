using API.Authorization;

namespace API.Extensions;

public static class PoliciesRegistration
{
    public static void RegisterPolicies(this IServiceCollection services)
    {
        var type = typeof(Permissions);
        var fields = type.GetFields();
        var permissions = fields.Select(f => f.GetValue(null) as string);
        var authorizationBuilder = services.AddAuthorizationBuilder();

        foreach (var permission in permissions)
        {
            authorizationBuilder.AddPolicy(permission!, policy =>
            {
                policy.Requirements.Add(new RbacRequirement(permission!));
            });
        }
    }
}