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
        group.MapPost("unassign", UnassignEndpointHandler.Handle)
            .WithMetadata(new SwaggerOperationAttribute("Unassign staff"))
            .RequireAuthorization();
        // Put methods
        group.MapPost("checkin-info", GetCheckinInfoEndpointHandler.Handle)
            .WithMetadata(new SwaggerOperationAttribute("Checkin a ticket"))
            .RequireAuthorization();
        group.MapPut("checkin", CheckinEndpointHandler.Handle)
            .WithMetadata(new SwaggerOperationAttribute("Checkin a ticket"))
            .RequireAuthorization();
        return group;
    }
}