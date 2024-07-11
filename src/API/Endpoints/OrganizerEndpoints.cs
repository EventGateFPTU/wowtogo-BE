using API.Endpoints.EndpointHandler.OrganizerEndpointHandler.Commands;
using API.Endpoints.EndpointHandler.OrganizerEndpointHandler.Queries;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Endpoints;
public static class OrganizerEndpoints
{
	public static RouteGroupBuilder MapOrganizerEndpoints(this RouteGroupBuilder group)
	{
		// GET
		group.MapGet("", GetAllOrganizerEndpointHandler.Handle).WithMetadata(new SwaggerOperationAttribute("Get all organizers"));
		// POST
		group.MapPost("", CreateOrganizerEndpointHandler.Handle).DisableAntiforgery().WithMetadata(new SwaggerOperationAttribute("Create organizer")).RequireAuthorization();
		// PUT
		group.MapPut("/{organizerId}", UpdateOrganizerEndpointHandler.Handle).DisableAntiforgery().WithMetadata(new SwaggerOperationAttribute("Update organizer"));
		group.MapPut("/{organizerId}/image", UploadOrganizerImageEndpointHandler.Handle).DisableAntiforgery().WithMetadata(new SwaggerOperationAttribute("Upload organizer image"));
		// DELETE
		group.MapDelete("/{organizerId}", DeleteOrganizerEndpointHandler.Handle).DisableAntiforgery().WithMetadata(new SwaggerOperationAttribute("Delete organizer"));
		return group;

	}
}