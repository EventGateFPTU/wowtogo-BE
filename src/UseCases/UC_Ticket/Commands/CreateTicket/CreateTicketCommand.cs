using Ardalis.Result;
using Domain.Models;
using Domain.Responses.Responses_Ticket;
using MediatR;

namespace UseCases.UC_Ticket.Commands.CreateTicket;
public record CreateTicketCommand(Guid TicketTypeId, Guid AttendeeId) : IRequest<Result<GetTicketDetailResponse>>
{

}