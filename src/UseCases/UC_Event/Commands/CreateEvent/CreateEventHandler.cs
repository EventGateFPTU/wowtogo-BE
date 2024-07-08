using Ardalis.Result;
using Domain.Interfaces.Data;
using Domain.Models;
using Domain.Models.Events;
using MediatR;
using UseCases.Common.Models;

namespace UseCases.UC_Event.Commands;
public class CreateEventHandler(IUnitOfWork unitOfWork, CurrentUser currentUser) : IRequestHandler<CreateEventCommand, Result>
{
    public async Task<Result> Handle(CreateEventCommand request, CancellationToken cancellationToken)
    {
        // TODO: check if categories exist
        IEnumerable<Category> categories = await unitOfWork.CategoryRepository.FindManyAsync(c => request.CategoryIds.Contains(c.Id), cancellationToken: cancellationToken);
        if (!categories.Any()) return Result.NotFound("Categories are not found");
        // TODO: check if user is already a organizer
        Organizer? organizer = await unitOfWork.OrganizerRepository.FindAsync(o => o.Id == currentUser.User.Id, cancellationToken: cancellationToken);
        if (organizer is null)
        {
            organizer = new()
            {
                Id = currentUser.User.Id,
                OrganizationName = currentUser.User.FirstName,
            };
            unitOfWork.OrganizerRepository.Add(organizer);
        }
        // TODO: create event
        Guid newEventId = Guid.NewGuid();
        Event newEvent = new()
        {
            Id = newEventId,
            Title = request.Title,
            Description = request.Description,
            Location = request.Location,
            MaxTickets = request.MaxTickets,
            OrganizerId = currentUser.User.Id,
            EventCategories = categories.Select(c => new EventCategory
            {
                EventId = newEventId,
                CategoryId = c.Id,
            }).ToList(),
        };
        unitOfWork.EventRepository.Add(newEvent);
        newEvent.AddDomainEvent(new EventCreatedEvent(newEvent.Id));
        if(!await unitOfWork.SaveChangesAsync(cancellationToken)) return Result.Error("Failed to create event");
        return Result.SuccessWithMessage("Event created successfully");
    }
}