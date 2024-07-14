using Ardalis.Result;
using Domain.Events.Staffs;
using Domain.Interfaces.Data;
using Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace UseCases.UC_Staff.Commands.AddStaff;
public class AddStaffHandler(IUnitOfWork unitOfWork) : IRequestHandler<AddStaffCommand, Result>
{
    public async Task<Result> Handle(AddStaffCommand request, CancellationToken cancellationToken = default)
    {
        User? checkingUser = await unitOfWork.UserRepository.FindAsync(u => u.Id.Equals(request.UserId), cancellationToken: cancellationToken);
        if (checkingUser is null) return Result.NotFound("User is not found");
        Event? checkingEvent = await unitOfWork.EventRepository.FindAsync(e => e.Id.Equals(request.EventId), cancellationToken: cancellationToken);
        if (checkingEvent is null) return Result.NotFound("Event is not found");
        Staff? checkingStaff = await unitOfWork.StaffRepository.FindAsync(s => s.UserId.Equals(request.UserId) && s.EventId.Equals(request.EventId), cancellationToken: cancellationToken);
        if (checkingStaff is not null) return Result.Error("Staff is already added");
        Staff newStaff = new Staff
        {
            UserId = checkingUser.Id,
            EventId = checkingEvent.Id,
        };
        unitOfWork.StaffRepository.Add(newStaff);
        newStaff.AddDomainEvent(new StaffAddedEvent(staffUserId: checkingUser.Id, eventId: checkingEvent.Id));
        if (!await unitOfWork.SaveChangesAsync(cancellationToken)) return Result.Error("Failed to add staff");
        return Result.SuccessWithMessage("Staff is added successfully");
    }
}