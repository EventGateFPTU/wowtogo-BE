namespace Domain.Responses.Responses_Order;
public record GetPaidOrdersResponse(IEnumerable<OrderResponse> Orders,
                                        int PageNumber,
                                        int PageSize);
public record PaidOrderDB(Guid Id, decimal TotalPrice, string Currency, DateTimeOffset CreatedAt);