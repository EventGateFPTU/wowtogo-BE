using Ardalis.Result;
using Domain.Interfaces.Data;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCases.UC_Event.Commands.UpdateEvent
{
    public class UpdateEventHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateEventCommand, Result>
    {
        public async Task<Result> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
        {
            Event? checkingEvent = await unitOfWork.EventRepository.FindAsync(e => e.Id.Equals(request.Id), trackChanges: true, cancellationToken: cancellationToken);
            if (checkingEvent is null) return Result.NotFound("Event is not found");
            // NOTE : update event information
            checkingEvent.Title = request.Title;
            checkingEvent.Description = request.Description;
            checkingEvent.Location = request.Location;
            checkingEvent.Status = request.Status;
            checkingEvent.UpdatedAt = DateTimeOffset.UtcNow;
            IEnumerable<EventCategory> eventCategories = await unitOfWork.EventCategoryRepository.FindManyAsync(ec => ec.EventId.Equals(request.Id));
            if (eventCategories.Any())
            {
                if (eventCategories.Count() != request.CategoryIds.Length) return Result.Error("Some categories is not found in database");
                unitOfWork.EventCategoryRepository.RemoveRange(eventCategories);
                unitOfWork.EventCategoryRepository.AddRange(request.CategoryIds.Select(ecId => new EventCategory()
                {
                    EventId = request.Id,
                    CategoryId = ecId,
                }));
            }
            if (!await unitOfWork.SaveChangesAsync()) return Result.Error("Failed to update event");
            return Result.Success();
        }
    }
}
