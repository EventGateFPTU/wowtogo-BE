using Domain.Events.Shared;

namespace Domain.Events.Staffs;

public class StaffAddedEvent(Guid staffUserId, Guid eventId): BaseEvent
{
    public Guid StaffUserId { get; set; } = staffUserId;
    public Guid EventId { get; set; } = eventId;
}