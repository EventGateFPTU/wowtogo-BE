namespace Domain.Responses.Responses_TicketType;

public record GetTicketTypesAndShowsOfEventResponse(
    Guid Id,
    string TicketTypeName,
    Guid ShowId,
    string ShowName,
    DateTimeOffset StartsAt,
    DateTimeOffset EndsAt,
    decimal Price,
    int Amount
    );