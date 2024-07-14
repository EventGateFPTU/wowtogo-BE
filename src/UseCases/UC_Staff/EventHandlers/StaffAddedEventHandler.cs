using Domain.Events.Staffs;
using MediatR;
using UseCases.Common.Constants;
using UseCases.Common.Contracts;

namespace UseCases.UC_Staff.EventHandlers;

public class StaffAddedEventHandler(IPermissionManager permissionManager) : INotificationHandler<StaffAddedEvent>
{
    public async Task Handle(StaffAddedEvent notification, CancellationToken cancellationToken)
    {
        var userObj = RelationObjects.User(notification.StaffUserId.ToString());
        var eventObj = RelationObjects.Event(notification.EventId.ToString());

        await permissionManager.PutPermission((userObj, Relations.EventStaff, eventObj));
    }
}