using Ardalis.Result;
using Domain.Enums;
using Domain.Interfaces.Data;
using Domain.Models;
using MediatR;
using UseCases.Common.Models;

namespace UseCases.UC_Show.Commands.DeleteShow;
public class DeleteShowHandler(IUnitOfWork unitOfWork, CurrentUser currentUser) : IRequestHandler<DeleteShowCommand, Result>
{
    public async Task<Result> Handle(DeleteShowCommand request, CancellationToken cancellationToken)
    {
        Show? checkingShow = await unitOfWork.ShowRepository.GetShowIncludingEventAsync(request.ShowId, cancellationToken: cancellationToken);
        if (checkingShow is null) return Result.NotFound("Show not found");

        Event? checkingEvent = await unitOfWork.EventRepository.GetEventWithOrganizer(checkingShow.EventId, cancellationToken: cancellationToken);
        if (checkingEvent is null) return Result.NotFound("Event is not found");
        if (!IsCurrentUserOrganizer(checkingEvent.Organizer)) return Result.Forbidden();

        if (checkingShow.Event?.Status == EventStatusEnum.Canceled) return Result.Error("Cannot delete show of a canceled event");
        if (checkingShow.Event?.Status == EventStatusEnum.Completed) return Result.Error("Cannot delete show of a completed event");
        if (checkingShow.Event?.Status == EventStatusEnum.Published) return Result.Error("Cannot delete show of a published event");
        if (checkingShow.TicketTypeShow.Any()) unitOfWork.TicketTypeShowRepository.RemoveRange(checkingShow.TicketTypeShow);
        unitOfWork.ShowRepository.Remove(checkingShow);
        if (!await unitOfWork.SaveChangesAsync(cancellationToken)) return Result.Error("Failed to delete show");
        return Result.SuccessWithMessage("Show is deleted successfully");
    }
    private bool IsCurrentUserOrganizer(Organizer organizer)
        => organizer.Id.Equals(currentUser.User!.Id);
}