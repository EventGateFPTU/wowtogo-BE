using Ardalis.Result;
using MediatR;

namespace UseCases.UC_Staff.Commands.Assign;

public record AssignCommand(
    Guid UserId,
    Guid ShowId
    ) : IRequest<Result>;