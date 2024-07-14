using Domain.Models;
using Domain.Responses.Responses_Attendee;

namespace UseCases.Mapper.Mapp_Attendee;

public static class AttendeeDetailResponseMapper
{
    public static AttendeeDetailResponse MapToAttendeeDetailResponse(this Attendee attendee, Ticket ticket)
        => new(
            Id: attendee.Id,
            Email: attendee.Email,
            LastName: attendee.LastName,
            FirstName: attendee.FirstName,
            PhoneNumber: attendee.PhoneNumber,
            DateOfBirth: attendee.DateOfBirth,
            TicketId: ticket.Id
            );
}