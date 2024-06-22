using API.Endpoints.EndpointHandler.TicketTypeEndpointHandler.Commands;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Endpoints;
public static class TicketTypeEndpoint
{
    public static RouteGroupBuilder MapTicketTypeEndpoints(this RouteGroupBuilder group)
    {
        // GET
        // POST
        group.MapPost("/", CreateTicketTypeEndpointHandler.Handle).WithMetadata(new SwaggerOperationAttribute("Create a ticket type"));
        // PUT
        // DELETE
        group.MapDelete("/", RemoveTicketTypeEndpointHandler.Handle).WithMetadata(new SwaggerOperationAttribute("Delete a ticket type"));
        return group;
    }
}