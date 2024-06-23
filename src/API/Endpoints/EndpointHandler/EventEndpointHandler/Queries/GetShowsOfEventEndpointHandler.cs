using Ardalis.Result;
using Domain.Responses.Responses_Show;
using MediatR;
using UseCases.UC_Show.Queries.GetShowsOfEvent;

namespace API.Endpoints.EndpointHandler.EventEndpointHandler.Queries;
public class GetShowsOfEventEndpointHandler
{
    public static async Task<Microsoft.AspNetCore.Http.IResult> Handle(ISender sender,
                                            Guid eventId,
                                            int pageNumber = 1,
                                            int pageSize = 10,
                                            CancellationToken cancellationToken = default)
    {
        Result<GetShowsOfEventResponse> result = await sender.Send(new GetShowsOfEventQuery(eventId, pageNumber, pageSize), cancellationToken);
        if (!result.IsSuccess)
        {
            if (result.Status == ResultStatus.NotFound) return Results.NotFound(result);
            return Results.BadRequest(result);
        }
        return Results.Ok(result);
    }
}