using Ardalis.Result;
using MediatR;
using static API.Endpoints.EndpointHandler.ShowEndpointHandler.Commands.UpdateShowEndpointHandler;
using UseCases.UC_Show.Commands.UpdateShow;
using UseCases.UC_Event.Commands.UpdateEvent;
using Domain.Enums;

namespace API.Endpoints.EndpointHandler.EventEndpointHandler.Commands
{
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
            if (!result.IsSuccess)
            {
                if (result.Status == ResultStatus.NotFound) return Results.NotFound(result);
                return Results.BadRequest(result);
            }
            return Results.NoContent();
        }
    }
    public record UpdateEventRequest(string Title,
        string Description,
        string Location,
        EventStatusEnum Status,
        Guid[] CategoryIds
    );
}
