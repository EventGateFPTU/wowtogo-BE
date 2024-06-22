namespace Domain.Responses.Responses_Staff;

public record StaffResponse(Guid Id,
        string Fullname,
        string Email
    );