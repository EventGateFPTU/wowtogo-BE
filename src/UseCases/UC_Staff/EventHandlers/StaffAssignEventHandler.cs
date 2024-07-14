using Domain.Events.Staffs;
using MediatR;
using UseCases.Common.Constants;
using UseCases.Common.Contracts;

namespace UseCases.UC_Staff.EventHandlers;

public class StaffAssignEventHandler(IPermissionManager permissionManager) : INotificationHandler<StaffAssignEvent>
{
    public async Task Handle(StaffAssignEvent notification, CancellationToken cancellationToken)
    {
        var showObj =RelationObjects.Show(notification.ShowId.ToString());
        var userObj = RelationObjects.User(notification.StaffUserId.ToString());
        
        await permissionManager.PutPermission((userObj, Relations.ShowAssignee, showObj));
    }
}