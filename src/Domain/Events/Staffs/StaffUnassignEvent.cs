using Domain.Events.Shared;

namespace Domain.Events.Staffs;

public class StaffUnassignEvent(Guid staffUserId, Guid showId): BaseEvent
{
    public Guid StaffUserId { get; set; } = staffUserId;
    public Guid ShowId { get; set; } = showId;
}