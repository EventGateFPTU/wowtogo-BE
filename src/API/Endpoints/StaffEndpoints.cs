using API.Endpoints.EndpointHandler.StaffEndpointHandler.Commands;
using API.Endpoints.EndpointHandler.StaffEndpointHandler.Queries;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Endpoints;
public static class StaffEndpoints
{
    public static RouteGroupBuilder MapStaffEndpoints(this RouteGroupBuilder group)
    {
        group.MapGet("/events", GetEventsByCurrentStaffEndpointHandler.Handle)
            .WithMetadata(new SwaggerOperationAttribute("Get Events Of Current Staff"))
            .RequireAuthorization();
        // Post methods
        group.MapPost("assign", AssignEndpointHandler.Handle)
            .WithMetadata(new SwaggerOperationAttribute("Assign staff to checkin for show"))
            .RequireAuthorization();
        // Put methods
        group.MapPut("checkin", CheckinEndpointHandler.Handle)
            .WithMetadata(new SwaggerOperationAttribute("Checkin a ticket"))
            .RequireAuthorization();
        return group;
    }
}