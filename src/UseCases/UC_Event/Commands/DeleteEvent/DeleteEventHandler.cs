using Ardalis.Result;
using Domain.Enums;
using Domain.Interfaces.Data;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCases.UC_Event.Commands.DeleteEvent
{
    public class DeleteEventHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteEventCommand, Result>
    {
        public async Task<Result> Handle(DeleteEventCommand request, CancellationToken cancellationToken)
        {
            Event? checkingEvent = await unitOfWork.EventRepository.FindAsync(e => e.Id.Equals(request.EventId), cancellationToken: cancellationToken);
            if (checkingEvent is null) return Result.NotFound("Event not found");
            if (checkingEvent.Status == EventStatusEnum.Draft) return Result.Error("Cannot delete a draft event");
            unitOfWork.EventRepository.Remove(checkingEvent);
            if (!await unitOfWork.SaveChangesAsync(cancellationToken)) return Result.Error("Failed to delete event");
            return Result.SuccessWithMessage("Event is deleted succesfully");
        }
    }
}
