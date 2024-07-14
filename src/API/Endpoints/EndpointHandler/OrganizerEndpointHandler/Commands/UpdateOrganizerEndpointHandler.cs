using Ardalis.Result;
using MediatR;
using UseCases.UC_Organizer.Commands.UpdateOrganizer;

namespace API.Endpoints.EndpointHandler.OrganizerEndpointHandler.Commands
{
	public class UpdateOrganizerEndpointHandler
	{
		public static async Task<Microsoft.AspNetCore.Http.IResult> Handle(ISender sender, Guid organizerId, UpdateOrganizerRequest request)
		{
			var result = await sender.Send(new UpdateOrganizerCommand(
								organizerId,
												request.OrganizationName,
																request.Description));
			if (!result.IsSuccess)
			{
				if (result.Status == ResultStatus.NotFound) return Results.NotFound(result);
				return Results.BadRequest(result);
			}
			return Results.Ok(result);
		}
	}
	public record UpdateOrganizerRequest(
						string OrganizationName,
						string Description);
}
