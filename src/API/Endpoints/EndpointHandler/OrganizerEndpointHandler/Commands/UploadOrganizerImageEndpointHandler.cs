using System.Reflection.Metadata;
using Ardalis.Result;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using MediatR;
using UseCases.UC_Organizer.Commands.UploadOrganizerImage;

namespace API.Endpoints.EndpointHandler.OrganizerEndpointHandler.Commands;
public class UploadOrganizerImageEndpointHandler
{
    public static async Task<Microsoft.AspNetCore.Http.IResult> Handle(ISender sender, Cloudinary cloudinary, Guid organizerId, IFormFile file)
    {
        ImageUploadParams uploadParams = new()
        {
            File = new FileDescription($"image-{organizerId}", file.OpenReadStream()),
            DisplayName = $"image-{organizerId}",
            Folder = "organizer",
            Overwrite = true,
        };
        ImageUploadResult? uploadingResult = cloudinary.Upload(uploadParams);
        if (uploadingResult is null) return Results.BadRequest("Failed to upload image");
        string imageUrl = uploadingResult.Url.ToString();
        Result result = await sender.Send(new UploadOrganizerImageCommand(organizerId, imageUrl));
        if (!result.IsSuccess)
        {
            if (result.Status == ResultStatus.NotFound) return Results.NotFound(result);
            return Results.BadRequest(result);
        }
        return Results.NoContent();
    }
}