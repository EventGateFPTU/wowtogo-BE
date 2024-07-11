using Ardalis.Result;
using MediatR;

namespace UseCases.UC_Organizer.Commands.DeleteOrganizer
{
	public record DeleteOrganizerCommand(Guid Id) : IRequest<Result>;
}
