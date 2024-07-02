using API.Endpoints.EndpointHandler.EventEndpointHandler.Commands;
using API.Endpoints.EndpointHandler.EventEndpointHandler.Queries;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Endpoints;
public static class EventEndpoints
{
    public static RouteGroupBuilder MapEventEndpoints(this RouteGroupBuilder group)
    {
        // GET
        group.MapGet("/{eventId}/staffs", GetEventStaffsEndpointHandler.Handle).WithMetadata(new SwaggerOperationAttribute("Get staffs in an event"));
        group.MapGet("/{eventId}/shows", GetShowsOfEventEndpointHandler.Handle).WithMetadata(new SwaggerOperationAttribute("Get shows in an event"));
        group.MapGet("/{eventId}", GetEventEndpointHandler.Handle).WithMetadata(new SwaggerOperationAttribute("Get event"));
        // POST
        group.MapPost("/{eventId}/staff/{userId}", AddStaffEndpointHandler.Handle).WithMetadata(new SwaggerOperationAttribute("Add a staff"));
        group.MapPost("", CreateEventEndpointHandler.Handle).WithMetadata(new SwaggerOperationAttribute("Create an event"));
        group.MapPost("/image-test", (Cloudinary cloudinary, IFormFile file) =>
        {
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(file.FileName, file.OpenReadStream()),
            };
            var uploadResult = cloudinary.Upload(uploadParams);
            return Results.Ok(uploadResult);
        }).DisableAntiforgery();
        // PUT
        // DELETE
        group.MapDelete("/staff/{staffId}", RemoveStaffEndpointHandler.Handle).WithMetadata(new SwaggerOperationAttribute("Delete a staff"));
        return group;
    }
}