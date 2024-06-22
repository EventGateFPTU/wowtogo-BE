namespace Domain.Responses.Responses_TicketType;
public record GetTicketTypeDetailsResponse(
    Guid Id,
    string ShowTitle,
    string Name,
    string Description,
    string ImageUrl,
    decimal Price,
    DateTimeOffset FromDate,
    DateTimeOffset ToDate,
    int Amount,
    int LeastAmountBuy,
    int MostAmountBuy,
    DateTimeOffset CreatedAt
);