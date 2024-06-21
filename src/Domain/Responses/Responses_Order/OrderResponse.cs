namespace Domain.Responses.Responses_Order;
public record OrderResponse(Guid Id,
                            decimal TotalPrice,
                            string Status,
                            string Currency,
                            DateTime CreatedAt);