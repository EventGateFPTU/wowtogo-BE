using Ardalis.Result;
using Domain.Interfaces.Data;
using Domain.Interfaces.Images;
using Domain.Models;
using MediatR;
using UseCases.Common.Models;

namespace UseCases.UC_Event.Commands.UploadBackgroundImage;
public class UploadBackgroundImageHandler(IUnitOfWork unitOfWork, IImageServices imageServices, CurrentUser currentUser) : IRequestHandler<UploadBackgroundImageCommand, Result>
{
    public async Task<Result> Handle(UploadBackgroundImageCommand request, CancellationToken cancellationToken = default)
    {
        string folderName = "event-background";
        string fileName = $"background-{request.EventId}";
        Event? checkingEvent = await unitOfWork.EventRepository.GetEventWithOrganizer(eventId: request.EventId, cancellationToken: cancellationToken);
        if (checkingEvent is null) return Result.NotFound("Event is not found");
        // NOTE: check if current user is organizer
        if (!IsCurrentUserOrganizer(organizer: checkingEvent.Organizer)) return Result.Forbidden();
        // NOTE: Update background URL of the event
        string imageUrl = await imageServices.Upload(request.fileStream, folderName, fileName);
        if (imageUrl is null) return Result.Error("Error in uploading image");
        checkingEvent.BackgroundImageUrl = imageUrl;
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.SuccessWithMessage("Background image is uploaded successfully.");
    }
    private bool IsCurrentUserOrganizer(Organizer organizer)
        => organizer.Id.Equals(currentUser.User!.Id);
}