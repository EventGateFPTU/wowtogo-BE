using Ardalis.Result;
using Domain.Interfaces.Data;
using Domain.Models;
using MediatR;

namespace UseCases.UC_Staff.Commands.AddStaff;
public class AddStaffHandler(IUnitOfWork unitOfWork) : IRequestHandler<AddStaffCommand, Result>
{
    public async Task<Result> Handle(AddStaffCommand request, CancellationToken cancellationToken = default)
    {
        User? checkingUser = await unitOfWork.UserRepository.FindAsync(u => u.Id.Equals(request.UserId), cancellationToken: cancellationToken);
        if (checkingUser is null) return Result.NotFound("User is not found");
        Event? checkingEvent = await unitOfWork.EventRepository.FindAsync(e => e.Id.Equals(request.EventId), cancellationToken: cancellationToken);
        if (checkingEvent is null) return Result.NotFound("Event is not found");
        Staff newStaff = new Staff
        {
            Id = checkingUser.Id,
            EventId = checkingEvent.Id,
        };
        unitOfWork.StaffRepository.Add(newStaff);
        if (!await unitOfWork.SaveChangesAsync(cancellationToken)) return Result.Error("Failed to add staff");
        return Result.SuccessWithMessage("Staff is added successfully");
    }
}