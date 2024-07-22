using Ardalis.Result;
using MediatR;
using UseCases.UC_Staff.Commands.AddStaff;

namespace API.Endpoints.EndpointHandler.EventEndpointHandler.Commands;
public class AddStaffEndpointHandler
{
    public static async Task<Microsoft.AspNetCore.Http.IResult> Handle(ISender sender, Guid userId, Guid eventId, CancellationToken cancellationToken = default)
    {
        Result result = await sender.Send(new AddStaffCommand(userId, eventId), cancellationToken);
        if (!result.IsSuccess)
        {
            if (result.Status == ResultStatus.NotFound)
                return Results.NotFound(result);
            if (result.Status == ResultStatus.Forbidden)
                return Results.Forbid();
            return Results.BadRequest(result);
        }
        return Results.Created(result.SuccessMessage, result);
    }
}