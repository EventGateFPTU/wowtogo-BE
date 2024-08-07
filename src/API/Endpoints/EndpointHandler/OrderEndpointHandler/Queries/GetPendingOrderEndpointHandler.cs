
using Ardalis.Result;
using Domain.Responses.Responses_Order;
using Domain.Responses.Shared;
using MediatR;
using UseCases.UC_Order.Queries.GetPendingOrders;

namespace API.Endpoints.EndpointHandler.OrderEndpointHandler.Queries;
public class GetPendingOrderEndpointHandler
{
    public static async Task<Microsoft.AspNetCore.Http.IResult> Handle(ISender sender,
                                                                        Guid userId,
                                                                        int pageNumber = 1,
                                                                        int pageSize = 10)
    {
        Result<PaginatedResponse<PendingOrderDB>> result = await sender.Send(new GetPendingOrdersQuery(
                       UserId: userId,
                       PageNumber: pageNumber,
                       PageSize: pageSize
                       ));
        if (result.IsSuccess) return Results.Ok(result);
        return Results.BadRequest(result);
    }
}