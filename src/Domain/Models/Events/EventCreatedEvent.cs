namespace Domain.Models.Events;

public class EventCreatedEvent(Guid eventId) : BaseEvent
{
    public Guid EventId { get; set; } = eventId;
}