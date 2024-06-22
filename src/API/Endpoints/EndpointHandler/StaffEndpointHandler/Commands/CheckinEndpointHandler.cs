using Ardalis.Result;
using MediatR;
using UseCases.UC_Staff.Commands.Checkin;

namespace API.Endpoints.EndpointHandler.StaffEndpointHandler.Commands;
public class CheckinEndpointHandler
{
    public static async Task<Microsoft.AspNetCore.Http.IResult> Handle(ISender sender, CheckinRequest request, CancellationToken cancellationToken = default)
    {
        Result result = await sender.Send(new CheckinCommand(request.Code, request.ShowId, request.usedInFormat), cancellationToken);
        if (!result.IsSuccess)
        {
            if (result.Status == ResultStatus.NotFound)
                return Results.NotFound(result);
            return Results.BadRequest(result);
        }
        return Results.NoContent();
    }
    public record CheckinRequest(Guid ShowId, string Code, string usedInFormat);
}