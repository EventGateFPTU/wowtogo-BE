using Ardalis.Result;
using Domain.Responses.Responses_Ticket;
using MediatR;

namespace UseCases.UC_Ticket.Queries.GetTicketDetail;
public record GetTicketDetailQuery(Guid TicketId) : IRequest<Result<GetTicketDetailResponse>>;