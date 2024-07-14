using Domain.Events.Shared;

namespace Domain.Events.Events;

public class EventCreatedEvent(Guid eventId, Guid currentUserId) : BaseEvent
{
    public Guid EventId { get; set; } = eventId;
    public Guid UserId { get; set; } = currentUserId;
}