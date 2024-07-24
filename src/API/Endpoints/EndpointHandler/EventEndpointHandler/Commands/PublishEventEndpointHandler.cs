using Ardalis.Result;
using MediatR;
using UseCases.UC_Event.Commands.PublishEvent;

namespace API.Endpoints.EndpointHandler.EventEndpointHandler.Commands;

public class PublishEventEndpointHandler
{
    public static async Task<Microsoft.AspNetCore.Http.IResult> Handle(ISender sender, Guid eventId)
    {
        Result result = await sender.Send(new PublishEventCommand(eventId));
        if (!result.IsSuccess)
        {
            if (result.Status == ResultStatus.Forbidden) return Results.Forbid();
            return Results.BadRequest(result);
        }
        return Results.NoContent();
    }
}