using Ardalis.Result;
using Domain.Interfaces.Data;
using Domain.Responses.Responses_Staff;
using MediatR;
using UseCases.Common.Constants;
using UseCases.Common.Contracts;

namespace UseCases.UC_Staff.Queries.GetShowStaffs;

public class GetShowStaffsHandler(IUnitOfWork unitOfWork, IPermissionManager permissionManager) : IRequestHandler<GetShowStaffsQuery, Result<List<StaffResponse>>>
{
    public async Task<Result<List<StaffResponse>>> Handle(GetShowStaffsQuery request, CancellationToken cancellationToken)
    {
        var targetShow = await unitOfWork.ShowRepository.FindAsync(e => e.Id.Equals(request.ShowId), cancellationToken: cancellationToken);
        if (targetShow is null) return Result.NotFound("show does not exist");

        var users = await permissionManager.ListUsersAsync("show", targetShow.Id.ToString(), Relations.ShowAssignee);

        var userIds = users.Select(Guid.Parse);

        var staffs = await unitOfWork.StaffRepository.GetStaffsByStaffIdsAsync(userIds.ToArray(), cancellationToken: cancellationToken);

        return Result.Success(staffs, "");
    }
}