using Ardalis.Result;
using Domain.Models;
using MediatR;
using UseCases.UC_Order.Commands.ConfirmPaidOrder;

namespace API.Endpoints.EndpointHandler.OrderEndpointHandler.Commands;
public class ConfirmPaidOrderHandler
{
    public static async Task<Microsoft.AspNetCore.Http.IResult> Handle(ISender sender, ConfirmPaidOrderRequest request)
    {
        Result<Ticket> result = await sender.Send(new ConfirmPaidOrderCommand(request.OrderId, request.UserId));
        if (!result.IsSuccess)
        {
            if (result.Errors.Any(e => e.Contains("not found", StringComparison.OrdinalIgnoreCase))) return Results.NotFound(result);
            return Results.BadRequest(result);
        }
        return Results.Created("", result);
    }
    public record ConfirmPaidOrderRequest(Guid OrderId, Guid UserId);
}