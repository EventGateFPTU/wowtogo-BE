namespace Domain.Responses.Responses_Show;
public record GetShowDetailResponse(
    Guid Id,
    string ShowTitle,
    string EventTitle,
    DateTimeOffset StartsAt,
    DateTimeOffset EndsAt
);