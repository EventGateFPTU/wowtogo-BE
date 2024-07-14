using Ardalis.Result;
using Domain.Interfaces.Data;
using MediatR;
using UseCases.Common.Constants;
using UseCases.Common.Contracts;
using UseCases.Common.Models;

namespace UseCases.UC_Staff.Commands.Assign;

public class AssignHandler(CurrentUser currentUser, IPermissionManager permissionManager): IRequestHandler<AssignCommand, Result>
{
    public async Task<Result> Handle(AssignCommand request, CancellationToken cancellationToken)
    {
        var userObj = RelationObjects.User(request.UserId.ToString());
        var showObj = RelationObjects.Show(request.ShowId.ToString());
        if (!await HasPermissionToAssignStaff(showObj, userObj)) 
            return Result.Forbidden();

        var isSuccess = await permissionManager.PutPermission((userObj, Relations.ShowAssignee, showObj));
        return isSuccess ? Result.SuccessWithMessage("Successfully assign staff permission") : Result.Error();
    }

    private async Task<bool> HasPermissionToAssignStaff(string showObj, string userObj)
    {
        var currentUserObj = RelationObjects.User(currentUser.User!.Id.ToString());
        var userIsStaffOfEvent = await permissionManager.HasPermission(
            userObj,
            Relations.CanBeAssignedShow,
            showObj
            );
        
        var hasPermission = await permissionManager.HasPermission(
            currentUserObj,
            Relations.CanAssignStaffShow,
            showObj
            );
        
        return userIsStaffOfEvent && hasPermission;
    }
}