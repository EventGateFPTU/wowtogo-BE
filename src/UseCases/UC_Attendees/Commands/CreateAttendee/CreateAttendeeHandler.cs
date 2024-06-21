using Ardalis.Result;
using Domain.Interfaces.Data;
using Domain.Models;
using MediatR;

namespace UseCases.UC_Attendees.Commands.CreateAttendee;
public class CreateAttendeeHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateAttendeeCommand, Result>
{
    public async Task<Result> Handle(CreateAttendeeCommand request, CancellationToken cancellationToken)
    {
        // Check if the user is not found
        User? user = await unitOfWork.UserRepository.FindAsync(u => u.Id.Equals(request.UserId), cancellationToken: cancellationToken);
        if (user is null) return Result.Error("User not found");
        Attendee attendee = new()
        {
            UserId = request.UserId,
            EventId = request.EventId,
            PhoneNumber = request.PhoneNumber,
            DateOfBirth = request.DateOfBirth,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
        };
        unitOfWork.AttendeeRepository.Add(attendee);
        if (!await unitOfWork.SaveChangesAsync(cancellationToken)) return Result.Error("Failed to create attendee");
        return Result.SuccessWithMessage("Create Attendee Successfully !");
    }
}