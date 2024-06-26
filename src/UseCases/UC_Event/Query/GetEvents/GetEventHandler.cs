using Ardalis.Result;
using Domain.Interfaces.Data;
using Domain.Models;
using Domain.Responses.Responses_Event;
using Domain.Responses.Responses_Order;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.UC_Order.Queries.GetPaidOrders;

namespace UseCases.UC_Event.Query.GetEvents
{
    public class GetEventHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetEventQuery, Result<GetEventResponse>>
    {
        public async Task<Result<GetEventResponse>> Handle(GetEventQuery request, CancellationToken cancellationToken)
        {
            Event? checkingEvent = await unitOfWork.EventRepository.FindAsync(u => u.Id.Equals(request.EventID), cancellationToken: cancellationToken);
            if (checkingEvent is null) return Result.NotFound("Event is not found");
            GetEventResponse? gettingEvent = await unitOfWork.EventRepository.GetEventAsync(eventId: request.EventID, cancellationToken: cancellationToken);
            if(gettingEvent is null) return Result.NotFound("No events are found");
            return Result.Success(gettingEvent);
        }
    }
    
}
