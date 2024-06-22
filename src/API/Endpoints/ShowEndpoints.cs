using API.Endpoints.EndpointHandler.TicketTypeEndpointHandler.Queries;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Endpoints;
public static class ShowEndpoints
{
    public static RouteGroupBuilder MapShowEndpoints(this RouteGroupBuilder group)
    {
        // GET
        group.MapGet("/{showId}/tickets", GetTicketTypesOfShowEndpointHandler.Handle).WithMetadata(new SwaggerOperationAttribute("Get all ticket types of a show"));

        return group;
    }
}