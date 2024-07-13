using Domain.Models;
using Domain.Responses.Responses_Order;

namespace UseCases.Mapper.Mapper_Order;
public static class CreateOrderResponseMapper
{
    public static CreateOrderResponse MapToCreateOrderResponse(this Order order)
        => new CreateOrderResponse(
            Id: order.Id,
            TotalPrice: order.TotalPrice,
            Status: order.Status.ToString(),
            Currency: order.Currency,
            TicketTypeId: order.TicketTypeId,
            UserId: order.UserId,
            CreatedAt: order.CreatedAt
        );
}