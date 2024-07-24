using Ardalis.Result;
using MediatR;

namespace UseCases.UC_Event.Commands.PublishEvent;

public record PublishEventCommand(Guid Id) : IRequest<Result>;