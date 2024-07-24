using Ardalis.Result;
using Domain.Interfaces.Data;
using Domain.Models;
using MediatR;
using UseCases.Common.Models;

namespace UseCases.UC_Event.Commands.PublishEvent;

public class PublishEventHandler(IUnitOfWork unitOfWork, CurrentUser currentUser) : IRequestHandler<PublishEventCommand, Result>
{
    public async Task<Result> Handle(PublishEventCommand request, CancellationToken cancellationToken)
    {
        Event? checkingEvent = await unitOfWork.EventRepository.GetEventWithOrganizer(request.Id, cancellationToken: cancellationToken, trackChanges: true);
        if (checkingEvent is null) return Result.NotFound("Event is not found");
        if (!IsCurrentUserOrganizer(checkingEvent)) return Result.Forbidden();
        if (checkingEvent.Status != Domain.Enums.EventStatusEnum.Draft &&
        checkingEvent.Status != Domain.Enums.EventStatusEnum.Canceled) return Result.Error($"Event is currently {checkingEvent.Status}, cannot published");
        checkingEvent.Publish();
        if (!await unitOfWork.SaveChangesAsync(cancellationToken)) return Result.Error("Update Failed !");
        return Result.SuccessWithMessage("Publish the event successfully !");
    }
    private bool IsCurrentUserOrganizer(Event checkingEvent)
        => checkingEvent.Organizer.Id.Equals(currentUser.User!.Id);
}