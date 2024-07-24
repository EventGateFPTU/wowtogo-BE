using Ardalis.Result;
using Domain.Interfaces.Data;
using Domain.Models;
using MediatR;
using UseCases.Common.Models;

namespace UseCases.UC_Event.Commands.CancelEvent;

public class CancelEventHandler(IUnitOfWork unitOfWork, CurrentUser currentUser) : IRequestHandler<CancelEventCommand, Result>
{
    public async Task<Result> Handle(CancelEventCommand request, CancellationToken cancellationToken)
    {
        Event? checkingEvent = await unitOfWork.EventRepository.GetEventWithOrganizer(request.EventId, cancellationToken: cancellationToken, trackChanges: true);
        if (checkingEvent is null) return Result.NotFound($"Event {request.EventId} is not found");
        if (!IsCurrentUserOwnsEvent(checkingEvent)) return Result.Forbidden();
        if (checkingEvent.Status != Domain.Enums.EventStatusEnum.Draft) return Result.Error($"Event {request.EventId} is {checkingEvent.Status}, can not be modified");
        checkingEvent.Cancel();
        if (!await unitOfWork.SaveChangesAsync(cancellationToken)) return Result.Error("Failed to cancel event");
        return Result.Success();
    }
    private bool IsCurrentUserOwnsEvent(Event checkingEvent)
        => checkingEvent.Organizer.Id.Equals(currentUser.User!.Id);
}