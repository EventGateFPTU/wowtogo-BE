using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UseCases.UC_Staff.Commands.Assign;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace API.Endpoints.EndpointHandler.StaffEndpointHandler.Commands;

public class AssignEndpointHandler
{
    public static async Task<IResult> Handle(ISender sender,AssignRequest request,
        CancellationToken cancellationToken = default)
    {
        var result = await sender.Send(new AssignCommand(
            UserId: request.UserId,
            ShowId: request.ShowId
            ), cancellationToken);

        if (result.IsSuccess) return Results.NoContent();
        if (result.Status == ResultStatus.Forbidden)
            return Results.BadRequest("No permission");
        return Results.BadRequest("Something went wrong");
    }

    public record AssignRequest(
        Guid UserId,
        Guid ShowId
        );
}