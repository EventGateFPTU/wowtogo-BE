namespace Domain.Responses.Responses_Order;

public record GetPendingOrdersResponse(IEnumerable<OrderResponse> Orders, int PageNumber, int PageSize);