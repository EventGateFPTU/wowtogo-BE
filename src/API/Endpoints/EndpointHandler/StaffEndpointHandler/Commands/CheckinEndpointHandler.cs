using Ardalis.Result;
using MediatR;
using UseCases.UC_Staff.Commands.Checkin;

namespace API.Endpoints.EndpointHandler.StaffEndpointHandler.Commands;
public class CheckinEndpointHandler
{
    public static async Task<Microsoft.AspNetCore.Http.IResult> Handle(ISender sender, CheckinRequest request, CancellationToken cancellationToken = default)
    {
        var result = await sender.Send(new CheckinCommand(request.Code, request.ShowId, request.UsedInFormat), cancellationToken);
        if (result.IsSuccess) return Results.Ok(result);
        return result.Status switch
        {
            ResultStatus.NotFound => Results.NotFound(result),
            ResultStatus.Forbidden => Results.Forbid(),
            _ => Results.BadRequest(result)
        };
    }
    public record CheckinRequest(Guid ShowId, string Code, string UsedInFormat);
}