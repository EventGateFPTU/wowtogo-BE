using API.Endpoints.EndpointHandler.StaffEndpointHandler.Commands;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Endpoints;
public static class StaffEndpoints
{
    public static RouteGroupBuilder MapStaffEndpoints(this RouteGroupBuilder group)
    {
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