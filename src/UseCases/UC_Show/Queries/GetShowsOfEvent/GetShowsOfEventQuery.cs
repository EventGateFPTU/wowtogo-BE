using Ardalis.Result;
using Domain.Responses.Responses_Show;
using MediatR;

namespace UseCases.UC_Show.Queries.GetShowsOfEvent;
public record GetShowsOfEventQuery(
    Guid EventId,
    int PageNumber,
    int PageSize
) : IRequest<Result<GetShowsOfEventResponse>>;