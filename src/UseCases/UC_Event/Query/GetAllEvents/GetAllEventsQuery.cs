using Ardalis.Result;
using Domain.Responses.Responses_Event;
using Domain.Responses.Shared;
using MediatR;

namespace UseCases.UC_Event.Query.GetAllEvents;

public record GetAllEventsQuery(int PageNumber = 1, int PageSize = 10, string? SearchTerm = null) : IRequest<Result<PaginatedResponse<GetEventResponse>>>;
