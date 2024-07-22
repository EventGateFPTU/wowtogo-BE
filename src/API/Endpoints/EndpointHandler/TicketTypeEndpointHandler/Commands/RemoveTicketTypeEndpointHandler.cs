using Ardalis.Result;
using MediatR;
using UseCases.UC_TicketType.Commands.DeleteTicketType;

namespace API.Endpoints.EndpointHandler.TicketTypeEndpointHandler.Commands;
public class RemoveTicketTypeEndpointHandler
{
    public static async Task<Microsoft.AspNetCore.Http.IResult> Handle(ISender sender, Guid ticketTypeId, CancellationToken cancellationToken = default)
    {
        Result result = await sender.Send(new DeleteTicketTypeCommand(ticketTypeId), cancellationToken);
        if (!result.IsSuccess)
        {
            if (result.Status == ResultStatus.NotFound) return Results.NotFound(result);
            if (result.Status == ResultStatus.Forbidden) return Results.Forbid();
            return Results.BadRequest(result);
        }
        return Results.NoContent();
    }
}