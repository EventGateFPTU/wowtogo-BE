using Ardalis.Result;
using MediatR;

namespace UseCases.UC_Organizer.Commands.UpdateOrganizer
{
	public record UpdateOrganizerCommand(
				Guid Id,
				string OrganizationName,
				string Description)
		: IRequest<Result>;
}
