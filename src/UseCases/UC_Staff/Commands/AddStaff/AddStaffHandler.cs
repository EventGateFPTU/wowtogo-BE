using Ardalis.Result;
using Domain.Events.Staffs;
using Domain.Interfaces.Data;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using UseCases.Common.Models;

namespace UseCases.UC_Staff.Commands.AddStaff;
public class AddStaffHandler(IUnitOfWork unitOfWork, CurrentUser currentUser) : IRequestHandler<AddStaffCommand, Result>
{
    public async Task<Result> Handle(AddStaffCommand request, CancellationToken cancellationToken = default)
    {
        User? checkingUser = await unitOfWork.UserRepository.FindAsync(u => u.Id.Equals(request.UserId), cancellationToken: cancellationToken);
        if (checkingUser is null) return Result.NotFound("User is not found");
        Event? checkingEvent = await unitOfWork.EventRepository.GetEventWithOrganizer(eventId: request.EventId, cancellationToken: cancellationToken);
        if (checkingEvent is null) return Result.NotFound("Event is not found");
        // NOTE: check if current user is organizer
        if (!IsCurrentUserOrganizer(organizer: checkingEvent.Organizer)) return Result.Forbidden();
        Staff? checkingStaff = await unitOfWork.StaffRepository.FindAsync(s => s.UserId.Equals(request.UserId) && s.EventId.Equals(request.EventId), cancellationToken: cancellationToken);
        if (checkingStaff is not null) return Result.Error("Staff is already added");
        Staff newStaff = new()
        {
            UserId = checkingUser.Id,
            EventId = checkingEvent.Id,
        };
        unitOfWork.StaffRepository.Add(newStaff);
        newStaff.AddDomainEvent(new StaffAddedEvent(staffUserId: checkingUser.Id, eventId: checkingEvent.Id));
        if (!await unitOfWork.SaveChangesAsync(cancellationToken)) return Result.Error("Failed to add staff");
        return Result.SuccessWithMessage("Staff is added successfully");
    }
    private bool IsCurrentUserOrganizer(Organizer organizer)
        => organizer.Id.Equals(currentUser.User!.Id);
}