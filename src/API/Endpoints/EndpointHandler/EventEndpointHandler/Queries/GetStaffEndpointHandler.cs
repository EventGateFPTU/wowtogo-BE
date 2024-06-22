using Ardalis.Result;
using Domain.Responses.Responses_Staff;
using MediatR;
using UseCases.UC_Staff.Queries.GetEventStaffs;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace API.Endpoints.EndpointHandler.EventEndpointHandler.Queries;

public class GetStaffEndpointHandler
{
    public static async Task<IResult> Handle(ISender sender,
        Guid eventId,
        int pageNumber = 1,
        int pageSize = 10)
    {
        var query = new GetEventStaffsQuery(eventId, pageNumber, pageSize);
        Result<GetEventStaffsResponse> result = await sender.Send(query);

        if (!result.IsSuccess) return Results.NotFound(result);
        return Results.Ok(result);
    }
}