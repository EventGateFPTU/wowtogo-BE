using Ardalis.Result;
using Domain.Responses.Responses_Event;
using Domain.Responses.Shared;
using MediatR;
using UseCases.UC_Event.Query.GetEventOfCurrentStaff;

namespace API.Endpoints.EndpointHandler.StaffEndpointHandler.Queries;
public class GetEventsByCurrentStaffEndpointHandler
{
    public static async Task<Microsoft.AspNetCore.Http.IResult> Handle(ISender sender, int pageNumber = 1, int pageSize = 10, CancellationToken cancellationToken = default)
    {
        Result<PaginatedResponse<GetEventResponse>> result = await sender.Send(new GetEventOfCurrentStaffQuery(
            PageNumber: pageNumber,
            PageSize: pageSize
        ), cancellationToken: cancellationToken);
        if (!result.IsSuccess) return Results.NotFound(result);
        return Results.Ok(result);
    }
}