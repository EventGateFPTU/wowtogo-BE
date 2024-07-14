namespace Domain.Responses.Responses_User;

public record PublicUserDetailResponse(
    Guid Id,
    string Email,
    string Name
    );