using Ardalis.Result;
using Domain.Interfaces.Images;
using MediatR;
using UseCases.UC_Event.Commands.UploadBackgroundImage;

namespace API.Endpoints.EndpointHandler.EventEndpointHandler.Commands;
public class UploadBackgroundImageEndpointHandler
{
    public static async Task<Microsoft.AspNetCore.Http.IResult> Handle(ISender sender, IImageServices imageServices, Guid eventId, IFormFile file)
    {
        Stream fileStream = file.OpenReadStream();
        if (imageServices.IsTooLarge(fileStream)) return Results.BadRequest(Result.Error("Your image should be under 10MB"));
        Result result = await sender.Send(new UploadBackgroundImageCommand(eventId, fileStream));
        if (!result.IsSuccess)
        {
            if (result.Status == ResultStatus.NotFound) return Results.NotFound(result);
            return Results.BadRequest(result);
        }
        return Results.NoContent();
    }
}