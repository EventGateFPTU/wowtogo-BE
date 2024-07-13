using Ardalis.Result;
using Domain.Interfaces.Data;
using Domain.Interfaces.Images;
using Domain.Models;
using MediatR;

namespace UseCases.UC_Event.Commands.UploadBannerImage;
public class UploadBannerImageHandler(IUnitOfWork unitOfWork, IImageServices imageServices) : IRequestHandler<UploadBannerImageCommand, Result>
{
    public async Task<Result> Handle(UploadBannerImageCommand request, CancellationToken cancellationToken)
    {
        string folderName = "event-banner";
        string fileName = $"event-{request.EventId}";
        Event? checkingEvent = await unitOfWork.EventRepository.FindAsync(e => e.Id.Equals(request.EventId), trackChanges: true, cancellationToken: cancellationToken);
        if (checkingEvent is null) return Result.NotFound("Event is not found");
        string imageUrl = await imageServices.Upload(request.fileStream, folderName, fileName);
        if (imageUrl is null) return Result.Error("Error in uploading image");
        checkingEvent.BannerImageUrl = imageUrl;
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.SuccessWithMessage("Banner image is uploaded successfully");
    }
}