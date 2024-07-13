using Domain.Events.Shared;

namespace Domain.Events.Shows;

public class ShowCreatedEvent(Guid EventId, Guid ShowId): BaseEvent
{
    public Guid EventId { get; set; }
    public Guid ShowId { get; set; }
}