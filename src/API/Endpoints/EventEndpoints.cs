using API.Endpoints.EndpointHandler.EventEndpointHandler.Commands;
using API.Endpoints.EndpointHandler.EventEndpointHandler.Queries;
using API.Endpoints.EndpointHandler.ShowEndpointHandler.Commands;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Endpoints;
public static class EventEndpoints
{
    public static RouteGroupBuilder MapEventEndpoints(this RouteGroupBuilder group)
    {
        // GET
        group.MapGet("/{eventId}/staffs", GetEventStaffsEndpointHandler.Handle)
            .WithMetadata(new SwaggerOperationAttribute("Get staffs in an event"))
            .RequireAuthorization();
        group.MapGet("/{eventId}/shows", GetShowsOfEventEndpointHandler.Handle)
            .WithMetadata(new SwaggerOperationAttribute("Get shows in an event"));
        group.MapGet("/{eventId}", GetEventEndpointHandler.Handle)
            .WithMetadata(new SwaggerOperationAttribute("Get event"));
        group.MapGet("/", GetAllEventsEndpointHandler.Handle)
            .WithMetadata(new SwaggerOperationAttribute("Get all events"));
        group.MapGet("/featured", GetAllFeaturedEventsEndpointHandler.Handle)
            .WithMetadata(new SwaggerOperationAttribute("Get featured events"));

        // POST
        group.MapPost("/{eventId}/staff/{userId}", AddStaffEndpointHandler.Handle)
            .WithMetadata(new SwaggerOperationAttribute("Add a staff"))
            .RequireAuthorization();
        group.MapPost("", CreateEventEndpointHandler.Handle)
            .WithMetadata(new SwaggerOperationAttribute("Create an event"))
            .RequireAuthorization();
        // PUT
        group.MapPut("/{eventId}", UpdateEventEndpointHandler.Handle)
            .WithMetadata(new SwaggerOperationAttribute("Update an event"));
        // .RequireAuthorization();
        group.MapPut("/{eventId}/background", UploadBackgroundImageEndpointHandler.Handle)
            .DisableAntiforgery()
            .WithMetadata(new SwaggerOperationAttribute("Upload background image of an event"))
            .RequireAuthorization();
        group.MapPut("/{eventId}/banner", UploadBannerImageEndpointHandler.Handle)
            .DisableAntiforgery()
            .WithMetadata(new SwaggerOperationAttribute("Upload background image of an event"))
            .RequireAuthorization();
        // DELETE
        group.MapDelete("/staff/{staffId}", RemoveStaffEndpointHandler.Handle)
            .WithMetadata(new SwaggerOperationAttribute("Delete a staff"))
            .RequireAuthorization();
        group.MapDelete("/{eventId}", DeleteEventEndpointHandler.Handle)
            .WithMetadata(new SwaggerOperationAttribute("Delete an Event"))
            .RequireAuthorization();
        return group;
    }
}