using Ardalis.Result;
using Domain.Responses.Responses_Event;
using MediatR;
using UseCases.UC_Event.Commands;

namespace API.Endpoints.EndpointHandler.EventEndpointHandler.Commands;
public class CreateEventEndpointHandler
{
    public static async Task<Microsoft.AspNetCore.Http.IResult> Handle(ISender sender, CreateEventRequest request, CancellationToken cancellationToken = default)
    {
        var result = await sender.Send(new CreateEventCommand(
            request.Title,
            request.Description,
            request.Location,
            request.MaxTickets,
            request.CategoryIds
        ), cancellationToken);
        if (result.IsSuccess) return Results.Ok(result);
        if(result.Status == ResultStatus.NotFound) return Results.NotFound(result);
        return Results.BadRequest(result);

    }
    public record CreateEventRequest(
    string Title,
    string Description,
    string Location,
    int MaxTickets,
    Guid[] CategoryIds);
}