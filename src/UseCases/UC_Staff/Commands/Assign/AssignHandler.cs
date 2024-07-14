using Ardalis.Result;
using Domain.Interfaces.Data;
using MediatR;
using UseCases.Common.Constants;
using UseCases.Common.Contracts;
using UseCases.Common.Models;

namespace UseCases.UC_Staff.Commands.Assign;

public class AssignHandler(IUnitOfWork unitOfWork, CurrentUser currentUser, IPermissionManager permissionManager): IRequestHandler<AssignCommand, Result>
{
    public async Task<Result> Handle(AssignCommand request, CancellationToken cancellationToken)
    {
        if (!HasPermissionToAssignStaff(request.ShowId)) return Result.Forbidden();
        // unitOfWork.
        throw new NotImplementedException();
    }

    private bool HasPermissionToAssignStaff(Guid showId)
    {
        // TODO: use permissionManager to check
        // permissionManager.HasPermission()
        
        // if show id not found in the event
        
        // if can assign
        return true;
    }
}