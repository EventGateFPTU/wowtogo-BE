using Ardalis.Result;
using Domain.Responses.Responses_Checkin;
using Domain.Responses.Responses_Ticket;
using Domain.Responses.Shared;
using MediatR;

namespace UseCases.UC_Ticket.Query.GetCheckinsByEvent;
public record GetCheckinsByEventQuery(
    Guid EventId,
    int PageNumber,
    int PageSize
) : IRequest<Result<PaginatedResponse<GetCheckinDetailResponse>>>;