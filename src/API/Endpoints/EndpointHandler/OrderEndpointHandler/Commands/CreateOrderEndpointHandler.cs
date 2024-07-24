using Ardalis.Result;
using Domain.Responses.Responses_Order;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UseCases.UC_Order.Commands.CreateOrder;

namespace API.Endpoints.EndpointHandler.OrderEndpointHandler.Commands;
public static class CreateOrderEndpointHandler
{
    public static async Task<Microsoft.AspNetCore.Http.IResult> Handle(ISender sender, [FromBody] CreateOrderRequest request)
    {
        Result<CreateOrderResponse> result = await sender.Send(
                        new CreateOrderCommand(request.TicketTypeId,
                                request.PhoneNumber,
                                request.FirstName,
                                request.LastName,
                                request.Email,
                                request.DateOfBirth)
            );
        if (!result.IsSuccess)
        {
            if (result.Status == ResultStatus.NotFound)
                return Results.NotFound(result);
            return Results.BadRequest(result);
        }
        return Results.Created(result.SuccessMessage, result);
    }
}

public record CreateOrderRequest
    (Guid TicketTypeId,
    string Email,
    string FirstName,
    string LastName,
    string PhoneNumber,
    DateTimeOffset DateOfBirth);