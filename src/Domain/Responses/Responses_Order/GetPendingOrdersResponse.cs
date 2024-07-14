namespace Domain.Responses.Responses_Order;

public record GetPendingOrdersResponse(IEnumerable<OrderResponse> Orders,
                                        int PageNumber,
                                        int PageSize);
public record PendingOrderDB(Guid id, decimal TotalPrice, string Currency, DateTimeOffset CreatedAt);