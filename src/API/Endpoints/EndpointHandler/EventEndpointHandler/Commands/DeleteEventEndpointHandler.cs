using Ardalis.Result;
using MediatR;
using UseCases.UC_Event.Commands.DeleteEvent;
using UseCases.UC_Show.Commands.DeleteShow;

namespace API.Endpoints.EndpointHandler.EventEndpointHandler.Commands
{
    public class DeleteEventEndpointHandler
    {
        public static async Task<Microsoft.AspNetCore.Http.IResult> Handle(ISender sender, Guid eventId, CancellationToken cancellationToken)
        {
            Result result = await sender.Send(new DeleteEventCommand(eventId), cancellationToken);
            if (!result.IsSuccess)
            {
                if (result.Status == ResultStatus.NotFound) return Results.NotFound(result);
                if (result.Status == ResultStatus.Forbidden) return Results.Forbid();
                return Results.BadRequest(result);
            }
            return Results.NoContent();
        }
    }
}
