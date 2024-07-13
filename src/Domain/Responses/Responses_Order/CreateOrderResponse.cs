namespace Domain.Responses.Responses_Order;
public record CreateOrderResponse(
    Guid Id,
    decimal TotalPrice,
    string Status,
    string Currency,
    Guid TicketTypeId,
    Guid UserId,
    DateTimeOffset CreatedAt
);