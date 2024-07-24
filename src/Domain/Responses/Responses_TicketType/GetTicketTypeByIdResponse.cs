namespace Domain.Responses.Responses_TicketType;

public record GetTicketTypeByIdResponse(
    Guid Id,
    string Name,
    string Description,
    string ImageUrl,
    decimal Price,
    DateTimeOffset FromDate,
    DateTimeOffset ToDate,
    int Amount,
    int LeastAmountBuy,
    int MostAmountBuy);