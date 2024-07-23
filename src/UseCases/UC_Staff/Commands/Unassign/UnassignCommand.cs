using Ardalis.Result;
using MediatR;

namespace UseCases.UC_Staff.Commands.Unassign;

public record UnassignCommand(Guid UserId, Guid ShowId) : IRequest<Result>;