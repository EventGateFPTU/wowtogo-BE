using Ardalis.Result;
using MediatR;

namespace UseCases.UC_Show.Commands.UpdateShow;
public record UpdateShowCommand(Guid Id,
                                Guid EventId,
                                string Title,
                                DateTimeOffset StartsAt,
                                DateTimeOffset EndsAt) : IRequest<Result>;