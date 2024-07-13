using Ardalis.Result;
using Domain.Responses.Responses_Show;
using MediatR;

namespace UseCases.UC_Show.Commands.CreateShow;
public record CreateShowCommand(Guid EventId,
                                Guid[] TicketTypeIds,
                                string Title,
                                DateTimeOffset StartsAt,
                                DateTimeOffset EndsAt) : IRequest<Result<CreateShowResponse>>;