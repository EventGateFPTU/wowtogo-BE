using Ardalis.Result;
using Domain.Models;
using MediatR;
using UseCases.UC_Order.Commands.ConfirmPaidOrder;

namespace API.Endpoints.EndpointHandler.OrderEndpointHandler.Commands;
public class ConfirmPaidOrderEndpointHandler
{
    public static async Task<Microsoft.AspNetCore.Http.IResult> Handle(ISender sender, Guid orderId)
    {
        Result<Ticket> result = await sender.Send(new ConfirmPaidOrderCommand(orderId));
        if (!result.IsSuccess)
        {
            if (result.Errors.Any(e => e.Contains("not found", StringComparison.OrdinalIgnoreCase))) return Results.NotFound(result);
            return Results.BadRequest(result);
        }
        return Results.Created(result.SuccessMessage, result);
    }
}