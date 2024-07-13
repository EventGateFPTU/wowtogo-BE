using Domain.Events.Shared;

namespace Domain.Events.Tickets;

public class TicketCreatedEvent(Guid TicketId, Guid TicketTypeId): BaseEvent
{
    public Guid TicketId { get; set; }
    public Guid TicketTypeId { get; set; }
}