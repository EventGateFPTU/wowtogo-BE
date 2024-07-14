namespace Domain.Responses.Responses_Attendee;

public record AttendeeDetailResponse(
    Guid Id,
    string FirstName,
    string LastName,
    string PhoneNumber,
    string Email,
    DateTimeOffset DateOfBirth,
    Guid TicketId
    );