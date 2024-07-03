using Ardalis.Result;
using Domain.Interfaces.Data;
using Domain.Models;
using MediatR;

namespace UseCases.UC_Event.Commands;
public class CreateEventHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateEventCommand, Result>
{
    public async Task<Result> Handle(CreateEventCommand request, CancellationToken cancellationToken)
    {
        // TODO: check if user exists
        User? user = await unitOfWork.UserRepository.FindAsync(u => u.Id.Equals(request.UserId), cancellationToken: cancellationToken);
        if (user is null) return Result.NotFound("User is not found");
        // TODO: check if categories exist
        IEnumerable<Category> categories = await unitOfWork.CategoryRepository.FindManyAsync(c => request.CategoryIds.Contains(c.Id), cancellationToken: cancellationToken);
        if (!categories.Any()) return Result.NotFound("Categories are not found");
        // TODO: check if user is already a organizer
        Guid organizerId = request.UserId;
        Organizer? organizer = await unitOfWork.OrganizerRepository.FindAsync(o => o.Id.Equals(organizerId), cancellationToken: cancellationToken);
        if (organizer is null)
        {
            organizer = new()
            {
                Id = request.UserId,
                OrganizationName = user.FirstName,
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
            OrganizerId = organizerId,
            EventCategories = categories.Select(c => new EventCategory
            {
                EventId = newEventId,
                CategoryId = c.Id,
            }).ToList(),
        };
        unitOfWork.EventRepository.Add(newEvent);
        if(!await unitOfWork.SaveChangesAsync(cancellationToken)) return Result.Error("Failed to create event");
        return Result.SuccessWithMessage("Event created successfully");
    }
}