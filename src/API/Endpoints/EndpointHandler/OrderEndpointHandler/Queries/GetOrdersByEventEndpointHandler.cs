using Ardalis.Result;
using Domain.Responses.Responses_Order;
using Domain.Responses.Shared;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using UseCases.UC_Order.Queries.GetOrdersByEvent;

namespace API.Endpoints.EndpointHandler.OrderEndpointHandler.Queries;
public class GetOrdersByEventEndpointHandler
{
    public static async Task<Microsoft.AspNetCore.Http.IResult> Handle(ISender sender, Guid eventId, int pageNumber = 1, int pageSize = 10)
    {
        Result<PaginatedResponse<OrderResponse>> result = await sender.Send(new GetOrdersByEventQuery(
                                                                            Id: eventId,
                                                                            PageNumber: pageNumber,
                                                                            pageSize));
        if (!result.IsSuccess) return Results.NotFound();
        return Results.Ok(result);
    }
}