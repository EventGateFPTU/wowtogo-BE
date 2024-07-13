using Ardalis.Result;
using Domain.Interfaces.Data;
using Domain.Interfaces.Images;
using Domain.Models;
using MediatR;

namespace UseCases.UC_Event.Commands.UploadBackgroundImage;
public class UploadBackgroundImageHandler(IUnitOfWork unitOfWork, IImageServices imageServices) : IRequestHandler<UploadBackgroundImageCommand, Result>
{
    public async Task<Result> Handle(UploadBackgroundImageCommand request, CancellationToken cancellationToken = default)
    {
        string folderName = "event-background";
        string fileName = $"background-{request.EventId}";
        Event? checkingEvent = await unitOfWork.EventRepository.FindAsync(e => e.Id.Equals(request.EventId), trackChanges: true, cancellationToken: cancellationToken);
        if (checkingEvent is null) return Result.NotFound("Event is not found");
        // NOTE: Update background URL of the event
        string imageUrl = await imageServices.Upload(request.fileStream, folderName, fileName);
        if (imageUrl is null) return Result.Error("Error in uploading image");
        checkingEvent.BackgroundImageUrl = imageUrl;
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.SuccessWithMessage("Background image is uploaded successfully.");
    }
}