using Ardalis.Result;
using Domain.Interfaces.Data;
using MediatR;
using UseCases.Common.Constants;
using UseCases.Common.Contracts;
using UseCases.Common.Models;

namespace UseCases.UC_Staff.Commands.Unassign;

public class UnassignHandler(CurrentUser currentUser, IPermissionManager permissionManager, IUnitOfWork unitOfWork) : IRequestHandler<UnassignCommand, Result>
{
    public async Task<Result> Handle(UnassignCommand request, CancellationToken cancellationToken)
    {
        var show = await unitOfWork.ShowRepository
            .FindAsync(x => x.Id == request.ShowId, cancellationToken: cancellationToken);

        if (show is null) return Result.NotFound("Show not found");
        
        var staff = await unitOfWork.StaffRepository
            .FindAsync(x => x.UserId == request.UserId && x.EventId == show.EventId, cancellationToken: cancellationToken);
        if (staff is null) return Result.NotFound("Staff not found");

        var staffShow = await unitOfWork.ShowStaffRepository
            .FindAsync(x => x.StaffId == staff.Id && x.ShowId == show.Id, cancellationToken: cancellationToken);

        if (staffShow is null) return Result.NotFound("Staff not found");
        
        var (canAssign, errorMsg) = await HasPermissionToAssignStaff(request.ShowId, request.UserId);
        if (!canAssign) return Result.Error(errorMsg);
        
        unitOfWork.ShowStaffRepository.Remove(staffShow);
        
        if (!await unitOfWork.SaveChangesAsync(cancellationToken)) return Result.Error("Cannot unassign this staff");
        
        var showObj =RelationObjects.Show(show.Id.ToString());
        var userObj = RelationObjects.User(staff.UserId.ToString());
        
        await permissionManager.RevokePermission((userObj, Relations.ShowAssignee, showObj));

        return Result.Success();
    }
    
    private async Task<(bool canAssign, string errorMsg)> HasPermissionToAssignStaff(Guid showId, Guid staffUserId)
    {
        var userObj = RelationObjects.User(staffUserId.ToString());
        var showObj = RelationObjects.Show(showId.ToString());
        var currentUserObj = RelationObjects.User(currentUser.User!.Id.ToString());

        var userIsAlreadyAssignee = await permissionManager.HasPermission(
            userObj,
            Relations.ShowAssignee,
            showObj
        );
        
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
        
        if ( !userIsAlreadyAssignee )
            return (false, "User is not already assigned");
        
        if ( !userIsStaffOfEvent )
            return (false, "User is not staff of event");
        
        if ( !hasPermission )
            return (false, "You don't have permission to assign staff");
        
        return (true, "");
    }
}