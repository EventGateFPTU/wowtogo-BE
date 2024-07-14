using Ardalis.Result;
using Domain.Responses.Responses_Event;
using Domain.Responses.Shared;
using MediatR;
using UseCases.UC_Event.Query.GetFeaturedEvent;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace API.Endpoints.EndpointHandler.EventEndpointHandler.Queries;

public class GetAllFeaturedEventsEndpointHandler
{
    public static async Task<IResult> Handle(ISender sender,
        int pageNumber = 1,
        int pageSize = 10,
        string? searchTerm = null)
    {
        Result<PaginatedResponse<EventDB>> result = await sender.Send(new GetFeaturedEventQuery(PageNumber: pageNumber,
            PageSize: pageSize,
            SearchTerm: searchTerm));
        if (result.IsSuccess)
            return Results.Ok(result);
        if (result.Status == ResultStatus.NotFound)
            return Results.NotFound(result);
        return Results.BadRequest(result);
    }
}