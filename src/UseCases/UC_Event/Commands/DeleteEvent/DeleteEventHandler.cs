using Ardalis.Result;
using Domain.Enums;
using Domain.Interfaces.Data;
using Domain.Models;
using MediatR;
using UseCases.Common.Models;

namespace UseCases.UC_Event.Commands.DeleteEvent;

public class DeleteEventHandler(IUnitOfWork unitOfWork, CurrentUser currentUser) : IRequestHandler<DeleteEventCommand, Result>
{
    public async Task<Result> Handle(DeleteEventCommand request, CancellationToken cancellationToken)
    {
        Event? checkingEvent = await unitOfWork.EventRepository.GetEventWithOrganizer(eventId: request.EventId, cancellationToken: cancellationToken);
        if (checkingEvent is null) return Result.NotFound("Event not found");
        if (!IsCurrentUserOrganizer(checkingEvent.Organizer)) return Result.Forbidden();
        if (checkingEvent.Status == EventStatusEnum.Published) return Result.Error("Can not delete a published event");
        if (checkingEvent.Status == EventStatusEnum.Completed) return Result.Error("Can not delete a completed event");
        if (checkingEvent.Status == EventStatusEnum.Canceled) return Result.Error("Can not delete a canceled event");
        unitOfWork.EventRepository.Remove(checkingEvent);
        if (!await unitOfWork.SaveChangesAsync(cancellationToken)) return Result.Error("Failed to delete event");
        return Result.SuccessWithMessage("Event is deleted succesfully");
    }
    private bool IsCurrentUserOrganizer(Organizer organizer)
        => organizer.Id.Equals(currentUser.User!.Id);
}
