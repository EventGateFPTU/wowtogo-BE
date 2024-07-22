using Ardalis.Result;
using Domain.Responses.Responses_Staff;
using MediatR;
using UseCases.UC_Staff.Queries.GetShowStaffs;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace API.Endpoints.EndpointHandler.ShowEndpointHandler.Queries;

public class GetShowStaffsHandler
{
    public static async Task<IResult> Handle(ISender sender, Guid showId, CancellationToken cancellationToken = default)
    {
        Result<Result<List<StaffResponse>>> result = await sender.Send(new GetShowStaffsQuery(showId), cancellationToken);
        if(!result.IsSuccess)
        {
            if(result.Status == ResultStatus.NotFound) return Results.NotFound(result);
            return Results.BadRequest(result);
        }
        return Results.Ok(result);
    }
}