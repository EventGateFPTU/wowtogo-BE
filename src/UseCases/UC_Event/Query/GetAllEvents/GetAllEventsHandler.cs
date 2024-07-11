using Ardalis.Result;
using Domain.Interfaces.Data;
using Domain.Responses.Responses_Event;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCases.UC_Event.Query.GetAllEvents
{
    public class GetAllEventsHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllEventsQuery, Result<GetAllEventsResponse>>
    {
        public async Task<Result<GetAllEventsResponse>> Handle(GetAllEventsQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<EventDB> eventDBs = await unitOfWork.EventRepository.GetAllEventAsync(request.PageNumber, request.PageSize, request.SearchTerm, false, cancellationToken);
            // if (!eventDBs.Any())  return Result.NotFound("Events are not found");
            GetAllEventsResponse result = new(request.PageNumber, request.PageSize, eventDBs);
            return Result.Success(result, "Events are found successfully");
        }
    }
}
