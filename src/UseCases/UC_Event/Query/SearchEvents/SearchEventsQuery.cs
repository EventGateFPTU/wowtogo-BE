using Domain.Responses.Responses_Event;
using Domain.Responses.Shared;
using MediatR;

namespace UseCases.UC_Event.Query.SearchEvents;

public record SearchEventsQuery(
    IEnumerable<Guid> CategoryIds,
    int PageNumber = 1,
    int PageSize = 10,
    string? SearchTerm = null,
    string? Location = null,
    DateTime? Date = null) : IRequest<PaginatedResponse<GetEventResponse>>;

