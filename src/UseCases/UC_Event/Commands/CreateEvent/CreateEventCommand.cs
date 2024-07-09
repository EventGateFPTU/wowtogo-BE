using Ardalis.Result;
using Domain.Responses.Responses_Event;
using MediatR;

namespace UseCases.UC_Event.Commands;
public record CreateEventCommand(
    string Title,
    string Description,
    string Location,
    int MaxTickets,
    Guid[] CategoryIds
) : IRequest<Result<GetEventResponse>>;