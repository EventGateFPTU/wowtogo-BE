using Ardalis.Result;
using Domain.Responses.Responses_Event;
using Domain.Responses.Shared;
using MediatR;
using UseCases.UC_Event.Query.GetAllEvents;

namespace API.Endpoints.EndpointHandler.EventEndpointHandler.Queries
{
    public class GetAllEventsEndpointHandler
    {
        public static async Task<Microsoft.AspNetCore.Http.IResult> Handle(ISender sender,
                                                                        int pageNumber = 1,
                                                                        int pageSize = 10,
                                                                        string? searchTerm = null)
        {
            Result<PaginatedResponse<GetEventResponse>> result = await sender.Send(new GetAllEventsQuery(PageNumber: pageNumber,
                                                                                         PageSize: pageSize,
                                                                                         SearchTerm: searchTerm));
            if (result.IsSuccess)
                return Results.Ok(result);
            if (result.Status == ResultStatus.NotFound)
                return Results.NotFound(result);
            return Results.BadRequest(result);
        }
    }
}
