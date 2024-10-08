using Ardalis.Result;
using Domain.Interfaces.Data;
using Domain.Responses.Responses_Event;
using Domain.Responses.Shared;
using MediatR;

namespace UseCases.UC_Event.Query.SearchEvents;

public class SearchEventsHandler(IUnitOfWork unitOfWork) : IRequestHandler<SearchEventsQuery, PaginatedResponse<GetEventResponse>>
{
    public async Task<PaginatedResponse<GetEventResponse>> Handle(SearchEventsQuery request, CancellationToken cancellationToken)
    {
        PaginatedResponse<GetEventResponse> eventDBs = await unitOfWork.EventRepository.SearchEventsAsync(request.CategoryIds,
            request.PageNumber, request.PageSize, request.SearchTerm, request.Location, request.Date, false,
            cancellationToken);
        return Result.Success(eventDBs, "Events are found successfully");
    }
}