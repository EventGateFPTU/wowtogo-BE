using Ardalis.Result;
using Domain.Interfaces.Data;
using Domain.Models;
using MediatR;

namespace UseCases.UC_Event.Commands.UploadBackgroundImage;
public class UploadBackgroundImageHandler(IUnitOfWork unitOfWork) : IRequestHandler<UploadBackgroundImageCommand, Result>
{
    public async Task<Result> Handle(UploadBackgroundImageCommand request, CancellationToken cancellationToken = default)
    {
        Event? checkingEvent = await unitOfWork.EventRepository.FindAsync(e => e.Id.Equals(request.EventId), trackChanges: true, cancellationToken: cancellationToken);
        if (checkingEvent is null) return Result.NotFound("Event is not found");
        // NOTE: Update background URL of the event
        checkingEvent.BackgroundImageUrl = request.Url;
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.SuccessWithMessage("Background image is uploaded successfully.");
    }
}