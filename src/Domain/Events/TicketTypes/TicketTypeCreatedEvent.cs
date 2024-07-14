using Domain.Events.Shared;

namespace Domain.Events.TicketTypes;

public class TicketTypeCreatedEvent(Guid eventId, Guid ticketTypeId) : BaseEvent
{
    public Guid EventId { get; set; } = eventId;
    public Guid TicketTypeId { get; set; } = ticketTypeId;
}