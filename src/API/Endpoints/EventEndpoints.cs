using API.Endpoints.EndpointHandler.EventEndpointHandler.Commands;
using API.Endpoints.EndpointHandler.EventEndpointHandler.Queries;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Endpoints;
public static class EventEndpoints
{
    public static RouteGroupBuilder MapEventEndpoints(this RouteGroupBuilder group)
    {
        // GET
        group.MapGet("/{eventId}/staffs", GetEventStaffsEndpointHandler.Handle).WithMetadata(new SwaggerOperationAttribute("Get staffs in an event"));
        group.MapGet("/{eventId}/shows", GetShowsOfEventEndpointHandler.Handle).WithMetadata(new SwaggerOperationAttribute("Get shows in an event"));
        // POST
        group.MapPost("/{eventId}/staff/{userId}", AddStaffEndpointHandler.Handle).WithMetadata(new SwaggerOperationAttribute("Add a staff"));
        // PUT
        // DELETE
        group.MapDelete("/staff/{staffId}", RemoveStaffEndpointHandler.Handle).WithMetadata(new SwaggerOperationAttribute("Delete a staff"));
        return group;
    }
}