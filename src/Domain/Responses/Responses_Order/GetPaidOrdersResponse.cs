namespace Domain.Responses.Responses_Order;
public record GetPaidOrdersResponse(IEnumerable<OrderResponse> Orders,
                                        int PageNumber,
                                        int PageSize);