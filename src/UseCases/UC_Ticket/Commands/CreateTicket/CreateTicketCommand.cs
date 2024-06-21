using Ardalis.Result;
using Domain.Models;
using MediatR;

namespace UseCases.UC_Ticket.Commands.CreateTicket;
public record CreateTicketCommand(Guid TicketTypeId, Guid AttendeeId) : IRequest<Result<Ticket>>
{

}