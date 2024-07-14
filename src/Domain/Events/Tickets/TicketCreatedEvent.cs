using Domain.Events.Shared;

namespace Domain.Events.Tickets;

public class TicketCreatedEvent(Guid ticketId, Guid ticketTypeId): BaseEvent
{
    public Guid TicketId { get; set; } = ticketId;
    public Guid TicketTypeId { get; set; } = ticketTypeId;
}