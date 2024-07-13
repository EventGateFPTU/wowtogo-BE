namespace Domain.Responses.Responses_Show;
public record CreateShowResponse(
    Guid Id,
    Guid EventId,
    string Title,
    DateTimeOffset StartsAt,
    DateTimeOffset EndsAt,
    DateTimeOffset CreatedAt
);