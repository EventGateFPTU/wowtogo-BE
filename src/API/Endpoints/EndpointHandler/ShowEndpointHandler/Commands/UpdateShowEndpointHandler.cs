using Ardalis.Result;
using MediatR;
using UseCases.UC_Show.Commands.UpdateShow;

namespace API.Endpoints.EndpointHandler.ShowEndpointHandler.Commands;
public class UpdateShowEndpointHandler
{
    public static async Task<Microsoft.AspNetCore.Http.IResult> Handle(ISender sender, Guid showId, UpdateShowRequest command, CancellationToken cancellationToken = default)
    {
        Result result = await sender.Send(new UpdateShowCommand(Id: showId,
                                                                EventId: command.EventId,
                                                                Title: command.Title,
                                                                StartsAt: command.StartsAt,
                                                                EndsAt: command.EndsAt), cancellationToken);
        if (!result.IsSuccess)
        {
            if (result.Status == ResultStatus.NotFound) return Results.NotFound(result);
            return Results.BadRequest(result);
        }
        return Results.NoContent();
    }
    public record UpdateShowRequest(Guid EventId,
                                    string Title,
                                    DateTimeOffset StartsAt,
                                    DateTimeOffset EndsAt
    );
}