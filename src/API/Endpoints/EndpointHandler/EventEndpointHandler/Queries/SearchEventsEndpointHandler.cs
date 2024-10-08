using Ardalis.Result;
using Domain.Responses.Responses_Event;
using Domain.Responses.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UseCases.UC_Event.Query.SearchEvents;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace API.Endpoints.EndpointHandler.EventEndpointHandler.Queries;

public class SearchEventsEndpointHandler
{
    public static async Task<IResult> Handle(ISender sender,
        [FromBody] SearchEventsParams searchEventsParams)
    {
        var query = new SearchEventsQuery(
            searchEventsParams.CategoryIds,
            searchEventsParams.PageNumber,
            searchEventsParams.PageSize,
            searchEventsParams.SearchTerm,
            searchEventsParams.Location,
            searchEventsParams.Date
            );
        Result<PaginatedResponse<GetEventResponse>> result = await sender.Send(query);

        if (!result.IsSuccess) return Results.NotFound(result);
        return Results.Ok(result);
    }

    public record SearchEventsParams(
        IEnumerable<Guid> CategoryIds,
        int PageNumber,
        int PageSize,
        string? SearchTerm,
        string? Location,
        DateTime? Date
        );
}