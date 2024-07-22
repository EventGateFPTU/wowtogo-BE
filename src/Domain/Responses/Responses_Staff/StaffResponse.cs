namespace Domain.Responses.Responses_Staff;

public record StaffResponse(Guid Id,
        Guid UserId,
        string Fullname,
        string Email
    );