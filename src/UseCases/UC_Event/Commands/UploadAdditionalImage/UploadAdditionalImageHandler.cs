using Ardalis.Result;
using Domain.Interfaces.Data;
using Domain.Interfaces.Images;
using Domain.Models;
using MediatR;
using Microsoft.Extensions.Options;
using UseCases.Common.Models;

namespace UseCases.UC_Event.Commands.UploadAdditionalImage;

public class UploadAdditionalImageHandler(IUnitOfWork unitOfWork, IImageServices imageServices, CurrentUser currentUser): IRequestHandler<UploadAdditionalImageCommand, Result>
{
    public async Task<Result> Handle(UploadAdditionalImageCommand request, CancellationToken cancellationToken)
    {
        string folderName= "event-additional-image";
        string fileName = $"additional-{request.EventId}";
        var checkingEvent = await unitOfWork.EventRepository.GetEventWithOrganizer(eventId: request.EventId, trackChanges: true, cancellationToken: cancellationToken);
        if (checkingEvent is null) return Result.NotFound("Event is not found");
        if (!IsCurrentUserOrganizer(organizer: checkingEvent.Organizer)) return Result.Forbidden();
        string imageUrl = await imageServices.Upload(request.FileStream, folderName, fileName);
        if (imageUrl == string.Empty) return Result.Error("Error in uploading image");
        var addedImgList = unitOfWork.AdditionalImageRepository
            .DBSet()
            .Count(a => a.EventId == request.EventId);
        if (addedImgList >= 5) return Result.Error("Exceeded number of additional images (limit is 5)");

        AdditionalImage newImg = new()
        {
            EventId = request.EventId,
            Url = imageUrl,
            SlotNumber = addedImgList+1,
        };

        unitOfWork.AdditionalImageRepository.Add(newImg);
        if (!await unitOfWork.SaveChangesAsync(cancellationToken)) return Result.Error("Failed to add additional image");
        return Result.SuccessWithMessage("Additional image added");
    }
    
    private bool IsCurrentUserOrganizer(Organizer organizer)
        => organizer.Id.Equals(currentUser.User!.Id);
}