﻿using Ardalis.Result;
using MediatR;
using UseCases.UC_Staff.Commands.Unassign;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace API.Endpoints.EndpointHandler.StaffEndpointHandler.Commands;

public class UnassignEndpointHandler
{
    public static async Task<IResult> Handle(ISender sender, UnassignRequest request,
        CancellationToken cancellationToken = default)
    {
        var result = await sender.Send(new UnassignCommand(
            UserId: request.UserId,
            ShowId: request.ShowId
        ), cancellationToken);

        if (result.IsSuccess) return Results.NoContent();
        if (result.Status == ResultStatus.Forbidden)
            return Results.BadRequest("No permission");
        return Results.BadRequest("Something went wrong");
    }
    
    public record UnassignRequest(
        Guid UserId,
        Guid ShowId
    );
}