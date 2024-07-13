using Ardalis.Result;
using Domain.Responses.Responses_Show;
using MediatR;
using UseCases.UC_Show.Commands.CreateShow;

namespace API.Endpoints.EndpointHandler.ShowEndpointHandler.Commands;
public class CreateShowEndpointHandler
{
    public static async Task<Microsoft.AspNetCore.Http.IResult> Handle(ISender sender, CreateShowRequest command, CancellationToken cancellationToken = default)
    {
        Result<CreateShowResponse> result = await sender.Send(new CreateShowCommand(EventId: command.EventId,
                                                                Title: command.Title,
                                                                StartsAt: command.StartsAt,
                                                                EndsAt: command.EndsAt), cancellationToken);
        if (!result.IsSuccess)
        {
            if (result.Status == ResultStatus.NotFound) return Results.NotFound(result);
            return Results.BadRequest(result);
        }
        return Results.Created(result.SuccessMessage, result);
    }
    public record CreateShowRequest(Guid EventId,
                                    string Title,
                                    DateTimeOffset StartsAt,
                                    DateTimeOffset EndsAt);
}