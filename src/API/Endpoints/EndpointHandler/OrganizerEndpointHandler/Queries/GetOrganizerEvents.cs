using Ardalis.Result;
using Domain.Responses.Responses_Event;
using MediatR;
using UseCases.UC_Event.Query.GetOrganizerEvents;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace API.Endpoints.EndpointHandler.OrganizerEndpointHandler.Queries;

public class GetOrganizerEvents
{
    public static async Task<IResult> Handle(ISender sender, int pageNumber = 1, int pageSize = 10, string? searchTerm = null)
    {
        Result<List<EventDB>> result = await sender.Send(new GetOrganizerEventsQuery());
        if (result.IsSuccess) return Results.Ok(result);
        if (result.Status == ResultStatus.NotFound) return Results.NotFound(result);
        return Results.BadRequest(result);
    }
}