using API.Endpoints.EndpointHandler.EventEndpointHandler.Commands;
using API.Endpoints.EndpointHandler.EventEndpointHandler.Queries;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Endpoints;
public static class EventEndpoints
{
    public static RouteGroupBuilder MapEventEndpoints(this RouteGroupBuilder group)
    {
        group.MapPost("/{eventId}/staff/{userId}", AddStaffEndpointHandler.Handle).WithMetadata(new SwaggerOperationAttribute("Add a staff"));
        group.MapDelete("/staff/{staffId}", RemoveStaffEndpointHandler.Handle).WithMetadata(new SwaggerOperationAttribute("Delete a staff"));
        group.MapGet("/{eventId}/staffs", GetStaffEndpointHandler.Handle).WithMetadata(new SwaggerOperationAttribute("Get staffs in an event"));
        // group.MapGet("/staff/{staffId}", )
        return group;
    }
}