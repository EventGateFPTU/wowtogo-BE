using MediatR;
using UseCases.UC_Event.Commands.LikeEvent;

namespace API.Endpoints.EndpointHandler.EventEndpointHandler.Commands;

public class LikeEventEndpointHandler
{
    public static async Task<IResult> Handle(ISender sender, Guid eventId,
        CancellationToken cancellationToken = default)
    {
        var result = await sender.Send(new LikeEventCommand(eventId), cancellationToken);
        
        return Results.Ok(result);
    }
}