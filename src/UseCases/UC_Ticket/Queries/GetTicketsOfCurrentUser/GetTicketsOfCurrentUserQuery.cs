using Ardalis.Result;
using Domain.Responses.Responses_Ticket;
using Domain.Responses.Shared;
using MediatR;

namespace UseCases.UC_Ticket.Queries.GetTicketsOfCurrentUser;
public record GetTicketsOfCurrentUserQuery(
    int PageNumber,
    int PageSize
) : IRequest<Result<PaginatedResponse<GetTicketDetailsResponse>>>;