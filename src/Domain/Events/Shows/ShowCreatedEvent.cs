using Domain.Events.Shared;

namespace Domain.Events.Shows;

public class ShowCreatedEvent(Guid eventId, Guid showId,Guid[] ticketTypeIds): BaseEvent
{
    public Guid EventId { get; set; } = eventId;
    public Guid ShowId { get; set; } = showId;
    public Guid[] TicketTypeIds { get; set; } = ticketTypeIds;
}