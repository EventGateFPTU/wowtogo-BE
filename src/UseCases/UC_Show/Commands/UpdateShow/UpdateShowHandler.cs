using Ardalis.Result;
using Domain.Interfaces.Data;
using Domain.Models;
using MediatR;
using UseCases.Common.Models;

namespace UseCases.UC_Show.Commands.UpdateShow;
public class UpdateShowHandler(IUnitOfWork unitOfWork, CurrentUser currentUser) : IRequestHandler<UpdateShowCommand, Result>
{
    public async Task<Result> Handle(UpdateShowCommand request, CancellationToken cancellationToken)
    {
        Show? checkingShow = await unitOfWork.ShowRepository.FindAsync(s => s.Id.Equals(request.Id), trackChanges: true, cancellationToken: cancellationToken);
        if (checkingShow is null) return Result.NotFound("Show is not found");
        Event? checkingEvent = await unitOfWork.EventRepository.FindAsync(e => e.Id.Equals(request.EventId));
        if (checkingEvent is null) return Result.NotFound("Event is not found");
        if (currentUser!.User!.Id != checkingEvent.OrganizerId) return Result.Error("You are not allowed to update the show");
        if (request.StartsAt >= request.EndsAt) return Result.Error("Starts at should be less than ends at");
        {
            checkingShow.EventId = request.EventId;
            checkingShow.Title = request.Title;
            checkingShow.StartsAt = request.StartsAt;
            checkingShow.EndsAt = request.EndsAt;
            checkingShow.UpdatedAt = DateTimeOffset.UtcNow;
        }
        if (!await unitOfWork.SaveChangesAsync()) return Result.Error("Failed to update show");
        return Result.Success();
    }
}