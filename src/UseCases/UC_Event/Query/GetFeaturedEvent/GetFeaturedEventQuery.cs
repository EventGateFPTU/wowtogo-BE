using Ardalis.Result;
using Domain.Responses.Responses_Event;
using Domain.Responses.Shared;
using MediatR;

namespace UseCases.UC_Event.Query.GetFeaturedEvent;

public record GetFeaturedEventQuery
(
    int PageNumber,
    int PageSize,
    string? SearchTerm
) : IRequest<Result<PaginatedResponse<EventDB>>>;