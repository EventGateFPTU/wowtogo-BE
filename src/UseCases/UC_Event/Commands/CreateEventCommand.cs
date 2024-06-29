using Ardalis.Result;
using MediatR;

namespace UseCases.UC_Event.Commands;
public record CreateEventCommand(
    string Title,
    string Description,
    string Location,
    Guid UserId,
    int MaxTickets,
    Guid[] CategoryIds
) : IRequest<Result>;