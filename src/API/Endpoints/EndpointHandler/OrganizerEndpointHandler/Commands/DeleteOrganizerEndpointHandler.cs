using Ardalis.Result;
using MediatR;
using UseCases.UC_Organizer.Commands.DeleteOrganizer;

namespace API.Endpoints.EndpointHandler.OrganizerEndpointHandler.Commands
{
	public class DeleteOrganizerEndpointHandler
	{
		public static async Task<Microsoft.AspNetCore.Http.IResult> Handle(ISender sender, Guid organizerId)
		{
			var result = await sender.Send(new DeleteOrganizerCommand(organizerId));
			if (!result.IsSuccess)
			{
				if (result.Status == ResultStatus.NotFound) return Results.NotFound(result);
				return Results.BadRequest(result);
			}
			return Results.Ok(result);
		}
	}
}
