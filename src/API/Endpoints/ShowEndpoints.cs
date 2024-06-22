using API.Endpoints.EndpointHandler.ShowEndpointHandler.Commands;
using API.Endpoints.EndpointHandler.TicketTypeEndpointHandler.Queries;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Endpoints;
public static class ShowEndpoints
{
    public static RouteGroupBuilder MapShowEndpoints(this RouteGroupBuilder group)
    {
        // GET
        group.MapGet("/{showId}/tickets", GetTicketTypesOfShowEndpointHandler.Handle).WithMetadata(new SwaggerOperationAttribute("Get all ticket types of a show"));
        // POST
        group.MapPost("", CreateShowEndpointHandler.Handle).WithMetadata(new SwaggerOperationAttribute("Create a show"));
        // PUT
        group.MapPut("/{showId}", UpdateShowEndpointHandler.Handle).WithMetadata(new SwaggerOperationAttribute("Update a show"));
        return group;
    }
}