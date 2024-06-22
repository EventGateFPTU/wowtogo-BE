using Ardalis.Result;
using MediatR;

namespace UseCases.UC_Show.Commands.CreateShow;
public record CreateShowCommand(Guid EventId,
                                string Title,
                                DateTimeOffset StartsAt,
                                DateTimeOffset EndsAt) : IRequest<Result>;