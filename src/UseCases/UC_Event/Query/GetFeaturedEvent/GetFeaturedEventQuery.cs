using Ardalis.Result;
using Domain.Responses.Responses_Event;
using MediatR;

namespace UseCases.UC_Event.Query.GetFeaturedEvent;

public record GetFeaturedEventQuery
(
    int PageNumber,
    int PageSize,
    string? SearchTerm
) : IRequest<Result<GetAllEventsResponse>>;