using Domain.Events.Shared;

namespace Domain.Events.Events;

public class EventCreatedEvent(Guid eventId) : BaseEvent
{
    public Guid EventId { get; set; } = eventId;
}