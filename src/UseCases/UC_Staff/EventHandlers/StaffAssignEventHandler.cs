using Domain.Events.Staffs;
using MediatR;

namespace UseCases.UC_Staff.EventHandlers;

public class StaffAssignEventHandler : INotificationHandler<StaffAssignEvent>
{
    public Task Handle(StaffAssignEvent notification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}