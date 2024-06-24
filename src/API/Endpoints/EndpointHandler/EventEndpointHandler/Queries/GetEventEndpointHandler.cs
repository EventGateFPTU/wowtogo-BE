using Ardalis.Result;
using Domain.Responses.Responses_Event;
using Domain.Responses.Responses_Staff;
using MediatR;
using UseCases.UC_Event.Query.GetEvents;
using UseCases.UC_Staff.Queries.GetEventStaffs;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace API.Endpoints.EndpointHandler.EventEndpointHandler.Queries
{
    public class GetEventEndpointHandler
    {
        public static async Task<IResult> Handle(ISender sender,
        Guid eventId)
        {
            var query = new GetEventQuery(eventId);
            Result<GetEventResponse> result = await sender.Send(query);

            if (!result.IsSuccess) return Results.NotFound(result);
            return Results.Ok(result);
        }
    }
}
