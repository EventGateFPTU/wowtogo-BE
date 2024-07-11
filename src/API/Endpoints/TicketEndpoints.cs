using API.Endpoints.EndpointHandler.TicketEndpointHandler.Queries;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Endpoints;
public static class TicketEndpoints
{
    public static RouteGroupBuilder MapTicketEndpoints(this RouteGroupBuilder group)
    {
        // Get methods
        group.MapGet("{ticketId}", GetTicketDetailEndpointHandler.Handle)
            .WithMetadata(new SwaggerOperationAttribute("Get ticket detail by ticket id"));
        return group;
    }
}