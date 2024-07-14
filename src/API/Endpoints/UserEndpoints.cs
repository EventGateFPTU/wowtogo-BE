using API.Endpoints.EndpointHandler.UserEndpointHandler;
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
        return group;
    }
}