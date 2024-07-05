using Ardalis.Result;
using Domain.Interfaces.Data;
using Domain.Models;
using MediatR;

namespace UseCases.UC_Event.Commands.UploadBannerImage;
public class UploadBannerImageHandler(IUnitOfWork unitOfWork) : IRequestHandler<UploadBannerImageCommand, Result>
{
    public async Task<Result> Handle(UploadBannerImageCommand request, CancellationToken cancellationToken)
    {
        Event? checkingEvent = await unitOfWork.EventRepository.FindAsync(e => e.Id.Equals(request.EventId), trackChanges: true, cancellationToken: cancellationToken);
        if (checkingEvent is null) return Result.NotFound("Event is not found");
        checkingEvent.BannerImageUrl = request.Url;
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.SuccessWithMessage("Banner image is uploaded successfully");
    }
}