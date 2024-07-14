using Ardalis.Result;
using Domain.Events.Staffs;
using Domain.Interfaces.Data;
using Domain.Models;
using MediatR;
using UseCases.Common.Constants;
using UseCases.Common.Contracts;
using UseCases.Common.Models;

namespace UseCases.UC_Staff.Commands.Assign;

public class AssignHandler(CurrentUser currentUser, IPermissionManager permissionManager, IUnitOfWork unitOfWork): IRequestHandler<AssignCommand, Result>
{
    public async Task<Result> Handle(AssignCommand request, CancellationToken cancellationToken)
    {
        if (!await HasPermissionToAssignStaff(request.ShowId, request.UserId)) 
            return Result.Forbidden();
        var staff = await unitOfWork.StaffRepository
            .FindAsync(x => x.UserId.Equals(request.UserId), cancellationToken: cancellationToken);
        if (staff is null) return Result.NotFound("Staff not found");
        var newShowStaff = new ShowStaff
        {
            UpdatedAt = DateTimeOffset.UtcNow,
            ShowId = request.ShowId,
            StaffId = staff.Id
        };
        unitOfWork.ShowStaffRepository.Add(newShowStaff);
        newShowStaff.AddDomainEvent(new StaffAssignEvent(
            staffUserId: request.UserId,
            showId: request.ShowId
            ));
        var isSuccess = await unitOfWork.SaveChangesAsync(cancellationToken);
        return isSuccess ? Result.SuccessWithMessage("Successfully assign staff permission") : Result.Error();
    }

    private async Task<bool> HasPermissionToAssignStaff(Guid showId, Guid staffUserId)
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
        
        return !userIsAlreadyAssignee 
               && userIsStaffOfEvent 
               && hasPermission;
    }
}