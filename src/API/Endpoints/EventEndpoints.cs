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
        group.MapGet("/{eventId}", GetEventEndpointHandler.Handle).WithMetadata(new SwaggerOperationAttribute("Get event"));
        // POST
        group.MapPost("/{eventId}/staff/{userId}", AddStaffEndpointHandler.Handle).WithMetadata(new SwaggerOperationAttribute("Add a staff"));
        group.MapPost("", CreateEventEndpointHandler.Handle).WithMetadata(new SwaggerOperationAttribute("Create an event"));
        // PUT
        group.MapPut("/{eventId}/background", UploadBackgroundImageEndpointHandler.Handle).DisableAntiforgery().WithMetadata(new SwaggerOperationAttribute("Upload background image of an event"));
        group.MapPut("/{eventId}/banner", UploadBannerImageEndpointHandler.Handle).DisableAntiforgery().WithMetadata(new SwaggerOperationAttribute("Upload background image of an event"));
        // DELETE
        group.MapDelete("/staff/{staffId}", RemoveStaffEndpointHandler.Handle).WithMetadata(new SwaggerOperationAttribute("Delete a staff"));
        return group;
    }
}