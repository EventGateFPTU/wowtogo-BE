using API.Endpoints.EndpointHandler.TicketTypeEndpointHandler.Commands;
using API.Endpoints.EndpointHandler.TicketTypeEndpointHandler.Queries;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Endpoints;
public static class TicketTypeEndpoint
{
    public static RouteGroupBuilder MapTicketTypeEndpoints(this RouteGroupBuilder group)
    {
        // GET
        group.MapGet("/", GetTicketTypesOfShowEndpointHandler.Handle).WithMetadata(new SwaggerOperationAttribute("Get all ticket types of a show"));
        // POST
        group.MapPost("/", CreateTicketTypeEndpointHandler.Handle).WithMetadata(new SwaggerOperationAttribute("Create a ticket type"));
        // PUT
        // DELETE
        group.MapDelete("/", RemoveTicketTypeEndpointHandler.Handle).WithMetadata(new SwaggerOperationAttribute("Delete a ticket type"));
        return group;
    }
}