using Ardalis.Result;
using Domain.Interfaces.Data;
using Domain.Models;
using MediatR;

namespace UseCases.UC_Organizer.Commands.UploadOrganizerImage;
public class UploadOrganizerImageHandler(IUnitOfWork unitOfWork) : IRequestHandler<UploadOrganizerImageCommand, Result>
{
    public async Task<Result> Handle(UploadOrganizerImageCommand request, CancellationToken cancellationToken)
    {
        Organizer? checkingOrganizer = await unitOfWork.OrganizerRepository
            .FindAsync(o => o.Id.Equals(request.OrganizerId), trackChanges: true, cancellationToken: cancellationToken);
        if (checkingOrganizer is null) return Result.NotFound("Organizer is not found");
        checkingOrganizer.ImageUrl = request.ImageUrl;
        await unitOfWork.SaveChangesAsync();
        return Result.SuccessWithMessage("Image uploaded successfully");
    }
}