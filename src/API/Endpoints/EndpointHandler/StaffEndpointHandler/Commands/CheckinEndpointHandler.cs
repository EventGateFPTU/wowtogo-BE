using Ardalis.Result;
using MediatR;
using UseCases.UC_Staff.Commands.CheckinAttendee;

namespace API.Endpoints.EndpointHandler.StaffEndpointHandler.Commands;
public class CheckinEndpointHandler
{
    public static async Task<Microsoft.AspNetCore.Http.IResult> Handle(ISender sender, CheckinRequest request, CancellationToken cancellationToken = default)
    {
        var result = await sender.Send(new CheckinAttendeeCommand(request.TicketId, request.ShowId, request.UsedInFormat), cancellationToken);
        if (result.IsSuccess) return Results.Ok(result);
        return result.Status switch
        {
            ResultStatus.NotFound => Results.NotFound(result),
            ResultStatus.Forbidden => Results.Forbid(),
            _ => Results.BadRequest(result)
        };
    }
    public record CheckinRequest(Guid ShowId, Guid TicketId, string UsedInFormat);
}