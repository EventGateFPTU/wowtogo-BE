using Ardalis.Result;
using Domain.Enums;
using Domain.Interfaces.Data;
using Domain.Models;
using Domain.Responses.Responses_Event;
using Domain.Responses.Responses_Order;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.Common.Models;
using UseCases.Mapper.Mapper_Event;
using UseCases.UC_Order.Queries.GetPaidOrders;

namespace UseCases.UC_Event.Query.GetEvents
{
    public class GetEventHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetEventQuery, Result<GetEventResponse>>
    {
        public async Task<Result<GetEventResponse>> Handle(GetEventQuery request, CancellationToken cancellationToken)
        {
            //check if user can view the event
            var getEventStaff = unitOfWork.StaffRepository.DBSet();
           /* var getStaff = await getEventStaff
                .Include(s => s.Id)
                .FirstOrDefaultAsync(s => s.Id.Equals(request.EventID));*/
            var getEventOrganizer = await unitOfWork.EventRepository.GetEventWithOrganizer(request.EventID);
            var gettingEvent = unitOfWork.EventRepository.DBSet();
            var eventDB = await gettingEvent
                .Include(e => e.Organizer)
                /*.Include(e => e.Staffs)*/
                .FirstOrDefaultAsync(e => e.Id.Equals(request.EventID));
            if (eventDB is null) return Result.NotFound("No events are found");
            if (eventDB.Id != getEventOrganizer!.Id /*|| getEvent.Id != getStaff.Id*/) return Result.Error("You are not authorized to view this event");
            return Result.Success(eventDB!.MapToGetEventResponse());
            

        }
    }
    
}
