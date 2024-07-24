using Ardalis.Result;
using MediatR;
using UseCases.UC_Event.Commands.CancelEvent;

namespace API.Endpoints.EndpointHandler.EventEndpointHandler.Commands;

public class CancelEventEndpointHandler
{
    public static async Task<Microsoft.AspNetCore.Http.IResult> Handle(ISender sender, Guid eventId, CancellationToken cancellationToken = default)
    {
        Result result = await sender.Send(new CancelEventCommand(eventId), cancellationToken);
        if (!result.IsSuccess)
        {
            if (result.Status == ResultStatus.Forbidden)
                return Results.Forbid();
            if (result.Status == ResultStatus.NotFound)
                return Results.NotFound();
            return Results.BadRequest(result);
        }
        return Results.NoContent();
    }
}