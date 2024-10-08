using Ardalis.Result;
using Domain.Enums;
using Domain.Interfaces.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UseCases.Common.Models;

namespace UseCases.UC_Event.Commands.LikeEvent;

public class LikeEventHandler(IUnitOfWork unitOfWork, CurrentUser currentUser): IRequestHandler<LikeEventCommand, Result>
{
    public async Task<Result> Handle(LikeEventCommand request, CancellationToken cancellationToken)
    {
        // Check if event can be liked? publish...
        var eventData = await unitOfWork.EventRepository.DBSet().AsNoTracking()
            .Where(e => e.Id == request.EventId && e.Status != EventStatusEnum.Draft)
            .FirstOrDefaultAsync(cancellationToken);
        if (eventData is null)
            return Result.NotFound();
        
        var userId = currentUser.User!.Id;
        var eventLikeRecord = await unitOfWork.LikeEventRepository.GetEventLike(
            userId: userId,
            eventId: request.EventId,
            cancellationToken: cancellationToken);

        string msg;
        if (eventLikeRecord is not null)
        {
            // Unlike
            unitOfWork.LikeEventRepository.Remove(eventLikeRecord);
            msg = $"Event {eventLikeRecord.EventId} is unliked by user {userId}";
        }
        else
        {
            // Like
            unitOfWork.LikeEventRepository.Add(new Domain.Models.LikeEvent
            {
                EventId = request.EventId,
                UserId = userId
            });
            msg = $"Event {request.EventId} is liked by user {userId}";
        }

        if (!await unitOfWork.SaveChangesAsync(cancellationToken))
            return Result.Invalid();
        return Result.SuccessWithMessage(msg);
    }
}