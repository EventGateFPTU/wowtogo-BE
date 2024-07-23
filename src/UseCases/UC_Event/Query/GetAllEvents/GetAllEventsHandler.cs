using Ardalis.Result;
using Domain.Interfaces.Data;
using Domain.Responses.Responses_Event;
using Domain.Responses.Shared;
using MediatR;

namespace UseCases.UC_Event.Query.GetAllEvents
{
    public class GetAllEventsHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllEventsQuery, Result<PaginatedResponse<GetEventResponse>>>
    {
        public async Task<Result<PaginatedResponse<GetEventResponse>>> Handle(GetAllEventsQuery request, CancellationToken cancellationToken)
        {
            PaginatedResponse<GetEventResponse> eventDBs = await unitOfWork.EventRepository.GetAllEventAsync(request.PageNumber, request.PageSize, request.SearchTerm, false, cancellationToken);
            return Result.Success(eventDBs, "Events are found successfully");
        }
    }
}
