using Ardalis.Result;
using MediatR;

namespace UseCases.UC_Event.Commands.LikeEvent;

public record LikeEventCommand(Guid EventId) : IRequest<Result>;