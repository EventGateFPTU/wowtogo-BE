using API.Endpoints.EndpointHandler.UserEndpointHandler.Queries;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Endpoints;
public static class UserEndpoints
{
    public static RouteGroupBuilder MapUserEndpoints(this RouteGroupBuilder group)
    {
        group.MapGet("", () => "hello users");
        group.MapGet("search", GetUsersByEmailEndpointHandler.Handle)
            .WithMetadata(new SwaggerOperationAttribute("Search user by email"))
            .RequireAuthorization();
        return group;
    }
}