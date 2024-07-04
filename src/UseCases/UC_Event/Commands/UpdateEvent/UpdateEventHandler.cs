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
            Organizer? checkingOrganizerId = await unitOfWork.OrganizerRepository.FindAsync(o => o.Id.Equals(request.OrganizerID));
            if (checkingOrganizerId is null) return Result.NotFound("OrganizerId is not found");
            {
                checkingEvent.Title = request.Title;
                checkingEvent.Description = request.Description;
                checkingEvent.Location = request.Location;
                checkingEvent.Status = request.Status;
                checkingEvent.OrganizerId = request.OrganizerID;
                checkingEvent.MaxTickets = request.MaxTickets;
                checkingEvent.UpdatedAt = DateTimeOffset.UtcNow;
            }
            if (!await unitOfWork.SaveChangesAsync()) return Result.Error("Failed to update event");
            return Result.Success();
        }
    }
}
