using Ardalis.Result;
using Domain.Models;
using MediatR;

namespace UseCases.UC_Organizer.Commands.UpdateOrganizer
{
	public record UpdateOrganizerCommand(
				Guid Id,
				string OrganizationName,
				string Description)
		: IRequest<Result<Organizer>>;
}
