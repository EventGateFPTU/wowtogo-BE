using Domain.Models;
using Domain.Responses.Responses_Order;

namespace UseCases.Mapper.Mapper_Order;
public static class OrderResponseMapper
{
    public static OrderResponse MapToOrderResponse(this Order order)
    {
        return new OrderResponse(
            Id: order.Id,
            TotalPrice: order.TotalPrice,
            Status: order.Status.ToString(),
            Currency: order.Currency,
            CreatedAt: order.CreatedAt,
            Email: order.User.Email
        );
    }
}