using Ardalis.Result;
using Domain.Interfaces.Images;
using MediatR;
using UseCases.UC_Event.Commands.UploadAdditionalImage;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace API.Endpoints.EndpointHandler.EventEndpointHandler.Commands;

public class UploadAdditionalImageEndpointHandler
{
    public static async Task<IResult> Handle(ISender sender, IImageServices imageServices, Guid eventId, IFormFile file)
    {
        Stream fileStream = file.OpenReadStream();
        if (imageServices.IsTooLarge(fileStream)) return Results.BadRequest(Result.Error("Your image should be under 10MB"));
        Result result = await sender.Send(new UploadAdditionalImageCommand(eventId, fileStream));
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