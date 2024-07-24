using Ardalis.Result;
using MediatR;
using UseCases.UC_Staff.Commands.RemoveStaff;

namespace API.Endpoints.EndpointHandler.EventEndpointHandler.Commands;
public class RemoveStaffEndpointHandler
{
    public static async Task<Microsoft.AspNetCore.Http.IResult> Handle(ISender sender, Guid staffId, CancellationToken cancellationToken = default)
    {
        Result result = await sender.Send(new RemoveStaffCommand(staffId), cancellationToken);
        if (!result.IsSuccess)
        {
            if (result.Status == ResultStatus.NotFound)
                return Results.NotFound(result);
            if (result.Status == ResultStatus.Forbidden)
                return Results.Forbid();
            return Results.BadRequest(result);
        }
        return Results.NoContent();
    }
}