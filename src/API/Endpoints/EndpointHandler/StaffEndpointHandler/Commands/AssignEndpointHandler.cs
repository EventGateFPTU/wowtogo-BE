using MediatR;
using Microsoft.AspNetCore.Mvc;
using UseCases.UC_Staff.Commands.Assign;

namespace API.Endpoints.EndpointHandler.StaffEndpointHandler.Commands;

public class AssignEndpointHandler
{
    public static async Task<IResult> Handle(ISender sender,[FromBody] AssignRequest request,
        CancellationToken cancellationToken = default)
    {
        var result = await sender.Send(new AssignCommand(
            UserId: request.UserId,
            ShowId: request.ShowId
            ), cancellationToken);

        if (result.IsSuccess) return Results.NoContent();
        return Results.BadRequest("Something went wrong");
    }

    public record AssignRequest(
        Guid UserId,
        Guid ShowId
        );
}