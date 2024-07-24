using API.Endpoints.EndpointHandler.TicketTypeEndpointHandler.Commands;
using API.Endpoints.EndpointHandler.TicketTypeEndpointHandler.Queries;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Endpoints;
public static class TicketTypeEndpoint
{
    public static RouteGroupBuilder MapTicketTypeEndpoints(this RouteGroupBuilder group)
    {
        // GET
        group.MapGet("{ticketTypeId:guid}", GetTicketTypeByIdHandler.Handle)
            .WithMetadata(new SwaggerOperationAttribute("Create a ticket type"));
        // POST
        group.MapPost("", CreateTicketTypeEndpointHandler.Handle)
            .WithMetadata(new SwaggerOperationAttribute("Create a ticket type"))
            .RequireAuthorization();
        // PUT
        group.MapPut("{ticketTypeId}", UpdateTicketTypeEndpointHandler.Handle)
            .WithMetadata(new SwaggerOperationAttribute("Update a ticket type"))
            .RequireAuthorization();
        // DELETE
        group.MapDelete("{ticketTypeId}", RemoveTicketTypeEndpointHandler.Handle)
            .WithMetadata(new SwaggerOperationAttribute("Delete a ticket type"))
            .RequireAuthorization();
        return group;
    }
}