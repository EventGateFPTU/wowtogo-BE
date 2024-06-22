using Ardalis.Result;
using MediatR;

namespace UseCases.UC_Attendees.Commands.CreateAttendee;
public record CreateAttendeeCommand(Guid UserId, Guid EventId, string PhoneNumber, DateTimeOffset DateOfBirth) : IRequest<Result>
{

}