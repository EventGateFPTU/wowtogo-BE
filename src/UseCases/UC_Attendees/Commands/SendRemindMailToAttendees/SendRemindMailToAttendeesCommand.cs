using Ardalis.Result;
using MediatR;

namespace UseCases.UC_Attendees.Commands.SendRemindMailToAttendees;

public record SendRemindMailToAttendeesCommand(DateTimeOffset FromTime, DateTimeOffset ToTime): IRequest<Result>;