using Domain.Events.Shared;

namespace Domain.Events.Shows;

public class ShowCreatedEvent(Guid EventId, Guid ShowId,Guid[] TicketTypeIds): BaseEvent
{
    public Guid EventId { get; set; }
    public Guid ShowId { get; set; }
    public Guid[] TicketTypeIds { get; set; } = [];
}