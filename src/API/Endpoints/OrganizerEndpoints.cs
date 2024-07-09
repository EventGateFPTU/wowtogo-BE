using API.Endpoints.EndpointHandler.OrganizerEndpointHandler;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Endpoints;
public static class OrganizerEndpoints
{
    public static RouteGroupBuilder MapOrganizerEndpoints(this RouteGroupBuilder group)
    {
        // PUT
        group.MapPut("/{organizerId}/image", UploadOrganizerImageEndpointHandler.Handle)
            .DisableAntiforgery()
            .WithMetadata(new SwaggerOperationAttribute("Upload organizer image"))
            .RequireAuthorization();
        return group;
    }
}