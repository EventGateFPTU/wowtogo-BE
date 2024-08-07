using Ardalis.Result;
using Domain.Events.Events;
using Domain.Interfaces.Data;
using Domain.Models;
using Domain.Responses.Responses_Event;
using MediatR;
using UseCases.Common.Models;

namespace UseCases.UC_Event.Commands.CreateEvent;
public class CreateEventHandler(IUnitOfWork unitOfWork, CurrentUser currentUser) : IRequestHandler<CreateEventCommand, Result<GetEventResponse>>
{
    public async Task<Result<GetEventResponse>> Handle(CreateEventCommand request, CancellationToken cancellationToken)
    {
        // check if categories exist
        var categories = await unitOfWork.CategoryRepository.FindManyAsync(c => request.CategoryIds.Contains(c.Id), cancellationToken: cancellationToken);

        // check if user is already a organizer
        Organizer? organizer = await unitOfWork.OrganizerRepository.FindAsync(o => o.Id == currentUser.User!.Id, cancellationToken: cancellationToken);
        if (organizer is null)
        {
            organizer = new()
            {
                Id = currentUser.User!.Id,
                OrganizationName = currentUser.User.FirstName,
            };
            unitOfWork.OrganizerRepository.Add(organizer);
        }
        // create event
        Guid newEventId = Guid.NewGuid();
        Event newEvent = new()
        {
            Id = newEventId,
            Title = request.Title,
            Description = request.Description,
            Location = request.Location,
            MaxTickets = request.MaxTickets,
            OrganizerId = currentUser.User!.Id,
            EventCategories = categories.Select(c => new EventCategory
            {
                EventId = newEventId,
                CategoryId = c.Id,
            }).ToList(),
        };
        // Open FGA
        unitOfWork.EventRepository.Add(newEvent);
        newEvent.AddDomainEvent(new EventCreatedEvent(eventId: newEvent.Id, currentUserId: currentUser.User.Id));

        if (!await unitOfWork.SaveChangesAsync(cancellationToken)) return Result.Error("Failed to create event");
        var createdEvent = await unitOfWork.EventRepository.GetEventAsync(newEventId, cancellationToken: cancellationToken);
        return createdEvent is not null ? Result.Success(createdEvent, "Event created successfully") : Result.Error("Failed to create event");
    }
}