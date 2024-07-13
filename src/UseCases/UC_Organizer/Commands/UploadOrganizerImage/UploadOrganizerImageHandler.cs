using Ardalis.Result;
using Domain.Interfaces.Data;
using Domain.Interfaces.Images;
using Domain.Models;
using MediatR;
using UseCases.Common.Models;

namespace UseCases.UC_Organizer.Commands.UploadOrganizerImage;
public class UploadOrganizerImageHandler(IUnitOfWork unitOfWork, IImageServices imageServices, CurrentUser currentUser) : IRequestHandler<UploadOrganizerImageCommand, Result>
{
    public async Task<Result> Handle(UploadOrganizerImageCommand request, CancellationToken cancellationToken)
    {
        Guid currentUserId = currentUser.User!.Id;
        string _folderName = "organizer";
        string _fileName = $"organizer-{currentUserId}";
        Organizer? checkingOrganizer = await unitOfWork.OrganizerRepository
            .FindAsync(o => o.Id.Equals(currentUserId), trackChanges: true, cancellationToken: cancellationToken);
        if (checkingOrganizer is null) return Result.NotFound("Organizer is not found");
        string imageUrl = await imageServices.Upload(request.imageStream, _folderName, _fileName);
        if (imageUrl is null or "") return Result.Error("Failed to upload to image service");
        checkingOrganizer.ImageUrl = imageUrl;
        await unitOfWork.SaveChangesAsync();
        return Result.SuccessWithMessage("Image uploaded successfully");
    }
}