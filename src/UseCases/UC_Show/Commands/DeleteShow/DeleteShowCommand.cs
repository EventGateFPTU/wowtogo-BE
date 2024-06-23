using Ardalis.Result;
using MediatR;

namespace UseCases.UC_Show.Commands.DeleteShow;
public record DeleteShowCommand(
    Guid ShowId
) : IRequest<Result>;