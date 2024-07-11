using Ardalis.Result;
using Domain.Responses.Responses_Organizer;
using MediatR;

namespace UseCases.UC_Organizer.Commands.CreateOrganizer
{
	public record CreateOrganizerCommand(
		string OrganizationName,
		string Description)
		: IRequest<Result<CreateOrganizerResponse>>;
}
