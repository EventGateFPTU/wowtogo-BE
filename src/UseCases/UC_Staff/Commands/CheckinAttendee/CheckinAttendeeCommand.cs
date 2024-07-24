using Ardalis.Result;
using Domain.Responses.Responses_Attendee;
using MediatR;

namespace UseCases.UC_Staff.Commands.CheckinAttendee;
public record CheckinAttendeeCommand(Guid TicketId, Guid ShowId, string UsedInFormat) : IRequest<Result<AttendeeDetailResponse>>;