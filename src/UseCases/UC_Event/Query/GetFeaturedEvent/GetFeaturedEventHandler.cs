using Ardalis.Result;
using Domain.Interfaces.Data;
using Domain.Models;
using Domain.Responses.Responses_Event;
using MediatR;

namespace UseCases.UC_Event.Query.GetFeaturedEvent;

public class GetFeaturedEventHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetFeaturedEventQuery,Result<GetAllEventsResponse>>
{
    public async Task<Result<GetAllEventsResponse>> Handle(GetFeaturedEventQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<EventDB> eventDBs = await unitOfWork.EventRepository.GetFeaturedEventsAsync(request.PageNumber, request.PageSize, request.SearchTerm, false, cancellationToken);
        // if (!eventDBs.Any())  return Result.NotFound("Events are not found");
        GetAllEventsResponse result = new(request.PageNumber, request.PageSize, eventDBs);
        return Result.Success(result, "Events are found successfully");
    }
}