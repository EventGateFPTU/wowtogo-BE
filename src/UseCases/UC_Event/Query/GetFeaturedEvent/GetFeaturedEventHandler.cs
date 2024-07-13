using Ardalis.Result;
using Domain.Interfaces.Data;
using Domain.Models;
using Domain.Responses.Responses_Event;
using Domain.Responses.Shared;
using MediatR;

namespace UseCases.UC_Event.Query.GetFeaturedEvent;

public class GetFeaturedEventHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetFeaturedEventQuery, Result<PaginatedResponse<EventDB>>>
{
    public async Task<Result<PaginatedResponse<EventDB>>> Handle(GetFeaturedEventQuery request, CancellationToken cancellationToken)
    {
        PaginatedResponse<EventDB> eventDBs = await unitOfWork.EventRepository.GetFeaturedEventsAsync(request.PageNumber, request.PageSize, request.SearchTerm, false, cancellationToken);
        return Result.Success(eventDBs, "Events are found successfully");
    }
}