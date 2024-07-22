using Ardalis.Result;
using Domain.Interfaces.Data;
using Domain.Models;
using MediatR;
using UseCases.Common.Models;

namespace UseCases.UC_Event.Commands.UpdateEvent
{
    public class UpdateEventHandler(IUnitOfWork unitOfWork, CurrentUser currentUser) : IRequestHandler<UpdateEventCommand, Result>
    {
        public async Task<Result> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
        {
            Event? checkingEvent = await unitOfWork.EventRepository.GetEventWithOrganizer(eventId: request.Id,
                                                                                        cancellationToken: cancellationToken,
                                                                                        trackChanges: true);
            if (checkingEvent is null) return Result.NotFound("Event is not found");

            if (!CanUpdate(checkingEvent)) return Result.Forbidden();
            // NOTE : check if event status is valid
            if (checkingEvent.Status == Domain.Enums.EventStatusEnum.Canceled) return Result.Unavailable("Event is cancelled");
            if (checkingEvent.Status == Domain.Enums.EventStatusEnum.Published) return Result.Unavailable("Event is already published");
            if (checkingEvent.Status == Domain.Enums.EventStatusEnum.Completed) return Result.Unavailable("Event is already completed");

            // NOTE : update event information
            checkingEvent.Title = request.Title;
            checkingEvent.Description = request.Description;
            checkingEvent.Location = request.Location;
            checkingEvent.Status = request.Status;
            checkingEvent.UpdatedAt = DateTimeOffset.UtcNow;
            if (request.CategoryIds.Length != 0)
            {
                IEnumerable<EventCategory> eventCategories = await unitOfWork.EventCategoryRepository.FindManyAsync(ec => ec.EventId.Equals(request.Id), cancellationToken: cancellationToken);
                IEnumerable<Category> checkingEventCategories = await unitOfWork.CategoryRepository
                    .FindManyAsync(c => request.CategoryIds.Contains(c.Id), cancellationToken: cancellationToken);
                if (checkingEventCategories.Count() != request.CategoryIds.Length) return Result.Error("Some categories is invalid !");
                unitOfWork.EventCategoryRepository.RemoveRange(eventCategories);
                unitOfWork.EventCategoryRepository.AddRange(request.CategoryIds.Select(ecId => new EventCategory()
                {
                    EventId = request.Id,
                    CategoryId = ecId,
                }));
            }


            if (!await unitOfWork.SaveChangesAsync(cancellationToken)) return Result.Error("Failed to update event");
            return Result.Success();
        }

        private bool CanUpdate(Event currentEvent)
        {
            var isOrganizer = currentEvent.Organizer.Id == currentUser.User!.Id;
            // check from OpenFGA
            return isOrganizer;
        }
    }
}
