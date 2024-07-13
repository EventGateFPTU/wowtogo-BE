using Ardalis.Result;
using Domain.Responses.Responses_TicketType;
using Domain.Responses.Shared;
using MediatR;

namespace UseCases.UC_TicketType.Queries.GetTicketTypesOfEvent;

public record GetTicketTypesOfEventQuery(
    Guid EventId,
    int PageNumber = 1,
    int PageSize = 10
    ) : IRequest<Result<PaginatedResponse<GetTicketTypesAndShowsOfEventResponse>>>;