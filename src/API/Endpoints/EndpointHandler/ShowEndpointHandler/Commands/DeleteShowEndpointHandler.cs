using Ardalis.Result;
using MediatR;
using UseCases.UC_Show.Commands.DeleteShow;

namespace API.Endpoints.EndpointHandler.ShowEndpointHandler.Commands;
public class DeleteShowEndpointHandler
{
    public static async Task<Microsoft.AspNetCore.Http.IResult> Handle(ISender sender, Guid showId, CancellationToken cancellationToken)
    {
        Result result = await sender.Send(new DeleteShowCommand(showId), cancellationToken);
        if (!result.IsSuccess)
        {
            if (result.Status == ResultStatus.NotFound) return Results.NotFound(result);
            return Results.BadRequest(result);
        }
        return Results.NoContent();
    }
}