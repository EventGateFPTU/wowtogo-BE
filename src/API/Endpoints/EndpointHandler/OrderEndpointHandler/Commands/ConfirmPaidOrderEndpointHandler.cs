using Ardalis.Result;
using Domain.Models;
using Domain.Responses.Responses_Ticket;
using MediatR;
using UseCases.UC_Order.Commands.ConfirmPaidOrder;

namespace API.Endpoints.EndpointHandler.OrderEndpointHandler.Commands;
public class ConfirmPaidOrderEndpointHandler
{
    public static async Task<Microsoft.AspNetCore.Http.IResult> Handle(ISender sender, Guid orderId)
    {
        Result<GetTicketDetailResponse> result = await sender.Send(new ConfirmPaidOrderCommand(OrderId: orderId));
        if (!result.IsSuccess)
        {
            if (result.Errors.Any(e => e.Contains("not found", StringComparison.OrdinalIgnoreCase))) return Results.NotFound(result);
            return Results.BadRequest(result);
        }
        return Results.Created(result.SuccessMessage, result);
    }
}