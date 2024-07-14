using Ardalis.Result;
using MediatR;
using UseCases.UC_Organizer.Commands.CreateOrganizer;

namespace API.Endpoints.EndpointHandler.OrganizerEndpointHandler.Commands
{
	public class CreateOrganizerEndpointHandler
	{
		public static async Task<Microsoft.AspNetCore.Http.IResult> Handle(ISender sender, CreateOrganizerRequest request)
		{
			var result = await sender.Send(new CreateOrganizerCommand(
				request.OrganizationName,
				request.Description
				));
			if (!result.IsSuccess)
			{
				if (result.Status == ResultStatus.NotFound) return Results.NotFound(result);
				return Results.BadRequest(result);
			}
			return Results.Ok(result);
		}
	}
	public record CreateOrganizerRequest(
				string OrganizationName,
						string Description);
}
