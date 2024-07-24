namespace UseCases.UC_Staff.Queries.GetCheckinInfo;

public record GetCheckinInfoResponse(
    Guid Id,
    string FirstName,
    string LastName,
    string PhoneNumber,
    string Email,
    DateTimeOffset DateOfBirth,
    Guid TicketId,
    bool CanCheckin);