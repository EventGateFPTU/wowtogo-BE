using System.Reflection.Metadata;
using Ardalis.Result;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using MediatR;
using UseCases.UC_Organizer.Commands.UploadOrganizerImage;

namespace API.Endpoints.EndpointHandler.OrganizerEndpointHandler.Commands;
public class UploadOrganizerImageEndpointHandler
{
    public static async Task<Microsoft.AspNetCore.Http.IResult> Handle(ISender sender, IFormFile file)
    {
        Result result = await sender.Send(new UploadOrganizerImageCommand(file.OpenReadStream()));
        if (!result.IsSuccess)
        {
            if (result.Status == ResultStatus.NotFound) return Results.NotFound(result);
            return Results.BadRequest(result);
        }
        return Results.NoContent();
    }
}