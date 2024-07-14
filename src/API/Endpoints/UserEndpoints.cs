using API.Endpoints.EndpointHandler.UserEndpointHandler;
using API.Endpoints.EndpointHandler.UserEndpointHandler.Queries;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Endpoints;
public static class UserEndpoints
{
    public static RouteGroupBuilder MapUserEndpoints(this RouteGroupBuilder group)
    {
        group.MapGet("/tickets", GetTicketsOfCurrentUserEndpointHandler.Handle)
            .WithMetadata(new SwaggerOperationAttribute("Get Tickets of Current User"))
            .RequireAuthorization();
        group.MapGet("/search", GetUsersByEmailEndpointHandler.Handle)
            .WithMetadata(new SwaggerOperationAttribute("Search users by email"))
            .RequireAuthorization();
        return group;
    }
}