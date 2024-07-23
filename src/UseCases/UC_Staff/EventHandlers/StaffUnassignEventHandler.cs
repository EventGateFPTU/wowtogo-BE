using Domain.Events.Staffs;
using MediatR;
using UseCases.Common.Constants;
using UseCases.Common.Contracts;

namespace UseCases.UC_Staff.EventHandlers;

public class StaffUnassignEventHandler(IPermissionManager permissionManager) : INotificationHandler<StaffUnassignEvent>
{
    public async Task Handle(StaffUnassignEvent notification, CancellationToken cancellationToken)
    {
        var showObj =RelationObjects.Show(notification.ShowId.ToString());
        var userObj = RelationObjects.User(notification.StaffUserId.ToString());
        
        await permissionManager.RevokePermission((userObj, Relations.ShowAssignee, showObj));
    }
}