using Ardalis.Result;
using MediatR;
using UseCases.UC_Event.Commands;

namespace API.Endpoints.EndpointHandler.EventEndpointHandler.Commands;
public class CreateEventEndpointHandler
{
    public static async Task<Microsoft.AspNetCore.Http.IResult> Handle(ISender sender, CreateEventRequest request, CancellationToken cancellationToken = default)
    {
        Result result = await sender.Send(new CreateEventCommand(
            request.Title,
            request.Description,
            request.Location,
            request.UserId,
            request.MaxTickets,
            request.CategoryIds
        ), cancellationToken);
        if(!result.IsSuccess){
            if(result.Status == ResultStatus.NotFound) return Results.NotFound(result);
            return Results.BadRequest(result);
        }
        return Results.Created();
    }
    public record CreateEventRequest(
    string Title,
    string Description,
    string Location,
    Guid UserId,
    int MaxTickets,
    Guid[] CategoryIds);
}