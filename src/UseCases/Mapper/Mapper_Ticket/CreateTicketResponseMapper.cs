using Domain.Models;
using Domain.Responses.Responses_Ticket;

namespace UseCases.Mapper.Mapper_Ticket;
public static class CreateTicketResponseMapper
{
    public static CreateTicketResponse MapToCreateTicketResponse(this Ticket ticket)
    {
        return new CreateTicketResponse(
                        Id: ticket.Id,
                        AttendeeId: ticket.AttendeeId,
                        TicketType: ticket.TicketType.Name,
                        EventName: ticket.TicketType.Show.Event.Title,
                        Code: ticket.Code,
                        CreatedAt: ticket.CreatedAt);
    }
}