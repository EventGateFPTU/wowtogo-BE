using Ardalis.Result;
using Domain.Responses.Responses_Attendee;
using MediatR;

namespace UseCases.UC_Staff.Commands.Checkin;
public record CheckinCommand(string Code, Guid ShowId, string UsedInFormat) : IRequest<Result<AttendeeDetailResponse>>;