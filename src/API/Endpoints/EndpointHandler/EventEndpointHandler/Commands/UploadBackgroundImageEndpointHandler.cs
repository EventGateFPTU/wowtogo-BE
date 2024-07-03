using Ardalis.Result;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Domain.Responses.Responses_Event;
using MediatR;
using UseCases.UC_Event.Commands.UploadBackgroundImage;
using UseCases.UC_Event.Query.GetEvents;

namespace API.Endpoints.EndpointHandler.EventEndpointHandler.Commands;
public class UploadBackgroundImageEndpointHandler
{
    public static async Task<Microsoft.AspNetCore.Http.IResult> Handle(ISender sender, Cloudinary cloudinary, Guid eventId, IFormFile file)
    {
        Result<GetEventResponse> gettingEvent = await sender.Send(new GetEventQuery(eventId));
        if (!gettingEvent.IsSuccess) return Results.NotFound(gettingEvent);
        ImageUploadParams uploadParams = new()
        {
            File = new FileDescription($"background-{eventId}", file.OpenReadStream()),
            Overwrite = true,
        };
        ImageUploadResult? uploadingResult = cloudinary.Upload(uploadParams);
        if (uploadingResult is null) return Results.BadRequest("Failed to upload image");
        string imageUrl = uploadingResult.Url.ToString();
        Result result = await sender.Send(new UploadBackgroundImageCommand(eventId, imageUrl));
        if (!result.IsSuccess)
        {
            if (result.Status == ResultStatus.NotFound) return Results.NotFound(result);
            return Results.BadRequest(result);
        }
        return Results.NoContent();
    }
}