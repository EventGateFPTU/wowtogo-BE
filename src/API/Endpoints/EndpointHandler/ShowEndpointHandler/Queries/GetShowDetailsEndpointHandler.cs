using Ardalis.Result;
using Domain.Responses.Responses_Show;
using MediatR;
using UseCases.UC_Show.Queries.GetShowDetails;

namespace API.Endpoints.EndpointHandler.ShowEndpointHandler.Queries;
public class GetShowDetailsEndpointHandler
{
    public static async Task<Microsoft.AspNetCore.Http.IResult> Handle(ISender sender, Guid showId, CancellationToken cancellationToken = default)
    {
        Result<GetShowDetailResponse> result = await sender.Send(new GetShowDetailsQuery(showId), cancellationToken);
        if(!result.IsSuccess)
        {
            if(result.Status == ResultStatus.NotFound) return Results.NotFound(result);
            return Results.BadRequest(result);
        }
        return Results.Ok(result);
    }
}