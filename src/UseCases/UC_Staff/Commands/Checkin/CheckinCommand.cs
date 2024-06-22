using Ardalis.Result;
using MediatR;

namespace UseCases.UC_Staff.Commands.Checkin;
public record CheckinCommand(string Code, Guid ShowId, string usedInFormat) : IRequest<Result>;