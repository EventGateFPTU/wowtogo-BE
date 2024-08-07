﻿using Ardalis.Result;
using MediatR;
using UseCases.UC_Event.Commands.UpdateEvent;
using Domain.Enums;

namespace API.Endpoints.EndpointHandler.EventEndpointHandler.Commands;

public class UpdateEventEndpointHandler
{
    public static async Task<Microsoft.AspNetCore.Http.IResult> Handle(ISender sender, Guid eventId, UpdateEventRequest command, CancellationToken cancellationToken = default)
    {
        Result result = await sender.Send(new UpdateEventCommand(Id: eventId,
                                                                Title: command.Title,
                                                                Description: command.Description,
                                                                Location: command.Location,
                                                                Status: command.Status,
                                                                CategoryIds: command.CategoryIds), cancellationToken);
        if (result.IsSuccess) return Results.NoContent();

        return result.Status switch
        {
            ResultStatus.NotFound => Results.NotFound(result),
            ResultStatus.Forbidden => Results.Forbid(),
            ResultStatus.Unavailable => Results.BadRequest(result),
            _ => Results.BadRequest(result)
        };
    }
}
public record UpdateEventRequest(string Title,
    string Description,
    string Location,
    EventStatusEnum Status,
    Guid[] CategoryIds
);
