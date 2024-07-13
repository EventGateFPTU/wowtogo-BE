namespace Domain.Responses.Responses_TicketType;
public record CreateTicketTypeResponse(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    DateTimeOffset FromDate,
    DateTimeOffset ToDate,
    int Amount,
    int LeastAmountBuy,
    int MostAmountBuy,
    DateTimeOffset CreatedAt
);