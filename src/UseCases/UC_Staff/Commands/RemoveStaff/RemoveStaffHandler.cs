using Ardalis.Result;
using Domain.Interfaces.Data;
using Domain.Models;
using MediatR;
using UseCases.Common.Models;

namespace UseCases.UC_Staff.Commands.RemoveStaff;
public class RemoveStaffHandler(IUnitOfWork unitOfWork, CurrentUser currentUser) : IRequestHandler<RemoveStaffCommand, Result>
{
    public async Task<Result> Handle(RemoveStaffCommand request, CancellationToken cancellationToken)
    {
        Staff? checkingStaff = await unitOfWork.StaffRepository.FindAsync(s => s.Id.Equals(request.StaffId), cancellationToken: cancellationToken);
        if (checkingStaff is null) return Result.NotFound("Staff is not found");
        Event? checkingEvent = await unitOfWork.EventRepository.GetEventWithOrganizer(checkingStaff.EventId, cancellationToken: cancellationToken);
        if (checkingEvent is null) return Result.NotFound("Event is not found");
        if (!IsCurrentUserOrganizerOrStaff(checkingEvent)) return Result.Forbidden();
        unitOfWork.StaffRepository.Remove(checkingStaff);
        if (!await unitOfWork.SaveChangesAsync(cancellationToken)) return Result.Error("Failed to remove staff");
        return Result.SuccessWithMessage("Staff is removed successfully");
    }
    private bool IsCurrentUserOrganizerOrStaff(Event checkingEvent)
        => checkingEvent.Organizer.Id.Equals(currentUser.User!.Id) || checkingEvent.Staffs.Any(s => s.UserId.Equals(currentUser.User!.Id));
}