using Ardalis.Result;
using MediatR;
using UseCases.UC_Order.Commands.CancelOrder;

namespace API.Endpoints.EndpointHandler.OrderEndpointHandler.Commands;
public class CancelOrderEndpointHandler
{
    public static async Task<Microsoft.AspNetCore.Http.IResult> Handle(ISender sender, Guid orderId)
    {
        Result result = await sender.Send(new CancelOrderCommand(orderId));
        if (!result.IsSuccess)
        {
            if (result.Status == ResultStatus.NotFound)
                return Results.NotFound(result);
            if (result.Status == ResultStatus.Forbidden)
                return Results.Forbid();
            return Results.BadRequest(result);
        }
        return Results.NoContent();
    }
}