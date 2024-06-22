using Ardalis.Result;
using Domain.Interfaces.Data;
using Domain.Models;
using MediatR;

namespace UseCases.UC_Staff.Commands.RemoveStaff;
public class RemoveStaffHandler(IUnitOfWork unitOfWork) : IRequestHandler<RemoveStaffCommand, Result>
{
    public async Task<Result> Handle(RemoveStaffCommand request, CancellationToken cancellationToken)
    {
        Staff? checkingStaff = await unitOfWork.StaffRepository.FindAsync(s => s.Id.Equals(request.StaffId), cancellationToken: cancellationToken);
        if (checkingStaff is null) return Result.NotFound("Staff is not found");
        unitOfWork.StaffRepository.Remove(checkingStaff);
        if (!await unitOfWork.SaveChangesAsync(cancellationToken)) return Result.Error("Failed to remove staff");
        return Result.SuccessWithMessage("Staff is removed successfully");
    }
}