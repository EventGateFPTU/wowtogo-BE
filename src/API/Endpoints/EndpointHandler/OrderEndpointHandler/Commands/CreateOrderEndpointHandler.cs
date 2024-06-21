using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UseCases.UC_Order.Commands.CreateOrder;

namespace API.Endpoints.EndpointHandler.OrderEndpointHandler.Commands;
public static class CreateOrderEndpointHandler
{
    public static async Task<Microsoft.AspNetCore.Http.IResult> Handle(ISender sender, [FromBody] CreateOrderRequest request)
    {
        Result result = await sender.Send(new CreateOrderQuery(request.TicketTypeId, request.UserId, request.Currency, request.TotalPrice));
        if (!result.IsSuccess)
        {
            if (result.Errors.Any(e => e.Contains("not found", StringComparison.OrdinalIgnoreCase))) return Results.NotFound(result);
            return Results.BadRequest(result);
        }
        return Results.Created("", result);
    }
}
public record CreateOrderRequest(Guid TicketTypeId, Guid UserId, string Currency, decimal TotalPrice);