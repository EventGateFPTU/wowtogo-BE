using Ardalis.Result;
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
            Event? checkingEvent = await unitOfWork.EventRepository.GetEventDeleteAsync(request.EventId, cancellationToken: cancellationToken);
            if (checkingEvent is null) return Result.NotFound("Event not found");
            
                return Result.SuccessWithMessage("Event is deleted succesfully");
        }
    }
}
