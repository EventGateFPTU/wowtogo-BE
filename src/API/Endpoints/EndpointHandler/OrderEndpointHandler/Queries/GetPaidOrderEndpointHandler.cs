using Ardalis.Result;
using Domain.Responses.Responses_Order;
using Domain.Responses.Shared;
using MediatR;
using UseCases.UC_Order.Queries.GetPaidOrders;

namespace API.Endpoints.EndpointHandler.OrderEndpointHandler.Queries;
public class GetPaidOrderEndpointHandler
{
    public static async Task<Microsoft.AspNetCore.Http.IResult> Handle(ISender sender,
                                                                        Guid userId,
                                                                        int pageNumber = 1,
                                                                        int pageSize = 10)
    {
        Result<PaginatedResponse<PaidOrderDB>> result = await sender.Send(new GetPaidOrdersQuery(UserId: userId,
                                                                                          PageNumber: pageNumber,
                                                                                          PageSize: pageSize));
        if (!result.IsSuccess) return Results.NotFound(result);
        return Results.Ok(result);
    }
}