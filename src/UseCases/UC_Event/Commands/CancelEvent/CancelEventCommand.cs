using Ardalis.Result;
using MediatR;

namespace UseCases.UC_Event.Commands.CancelEvent;

public record CancelEventCommand(Guid EventId) : IRequest<Result>;