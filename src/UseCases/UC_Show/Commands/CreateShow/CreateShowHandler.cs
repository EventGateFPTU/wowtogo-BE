using Ardalis.Result;
using Domain.Events.Shows;
using Domain.Interfaces.Data;
using Domain.Models;
using Domain.Responses.Responses_Show;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UseCases.Common.Models;
using UseCases.Mapper.Mapper_Show;

namespace UseCases.UC_Show.Commands.CreateShow;
public class CreateShowHandler(IUnitOfWork unitOfWork, CurrentUser currentUser) : IRequestHandler<CreateShowCommand, Result<CreateShowResponse>>
{
    public async Task<Result<CreateShowResponse>> Handle(CreateShowCommand request, CancellationToken cancellationToken)
    {
        Event? checkingEvent = await unitOfWork.EventRepository.GetEventWithOrganizer(request.EventId, cancellationToken: cancellationToken);
        if (checkingEvent is null) return Result.NotFound("Event is not found");
        if (!IsCurrentUserOwnEvent(checkingEvent)) return Result.Forbidden();
        if (request.StartsAt >= request.EndsAt) return Result.Error("Starts at should be less than ends at");
        IEnumerable<TicketType> checkingTicketTypes = await unitOfWork.TicketTypeRepository.FindManyAsync(tt => request.TicketTypeIds.Contains(tt.Id), cancellationToken: cancellationToken);
        if (checkingTicketTypes.Count() != request.TicketTypeIds.Length) return Result.Error("Some ticket type is not found");
        Guid newShowId = Guid.NewGuid();
        Show show = new()
        {
            EventId = request.EventId,
            Title = request.Title,
            StartsAt = request.StartsAt,
            EndsAt = request.EndsAt,
            TicketTypeShow = request.TicketTypeIds.Select(tt => new TicketTypeShow()
            {
                TicketTypeId = tt,
                ShowId = newShowId
            }).ToList()
        };
        var dbSet = unitOfWork.ShowRepository.DBSet() as DbSet<Show>;
        var addEntry = dbSet.Add(show);
        var showEvent = new ShowCreatedEvent(
            eventId: request.EventId,
            showId: addEntry.Entity.Id,
            ticketTypeIds: request.TicketTypeIds
        );
        show.AddDomainEvent(showEvent);
        if (!await unitOfWork.SaveChangesAsync(cancellationToken)) return Result.Error("Failed to create show");
        return Result.Success(show.MapToCreateShowResponse(), "Show is created successfully");
    }
    private bool IsCurrentUserOwnEvent(Event checkingEvent)
        => checkingEvent.Organizer.Id.Equals(currentUser.User!.Id);
}